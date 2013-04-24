using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Aramis;
using Aramis.DatabaseConnector;
using Aramis.SystemConfigurations;

namespace AtosFMCG.HelperClasses.Deleted
    {
    /// <summary>Удаление помеченых</summary>
    public class DeleteMarked
        {
        #region Шаблоны запроса в БД
        private const string query_catalogs_format = @"with MarkForDeletingObjects as(
    select Id, Description MarkForDeletingObjectName 
    from {0} 
    where Deleted=1 AND Id=@Id)
, TotalResult as(
    {1})

select 
    MarkForDeletingObjectId,
    ISNULL(ObjectHolderId, 0) ObjectHolderId,
    TableName,
    SubTableName,
    FieldName,
    ISNULL(RowNumber, 0) RowNumber 
from TotalResult
order by TableName,SubTableName,FieldName,RowNumber,ObjectHolderId";

        private const string query_catalogs_holders_format = @"select 
    MarkForDeletingObjects.Id MarkForDeletingObjectId,
    {3}.Id{4} ObjectHolderId,
    '{0}' TableName,
    '{1}' SubTableName,
    '{2}' FieldName,
    {5} as RowNumber 
from MarkForDeletingObjects
left join {3} on MarkForDeletingObjects.Id = [{3}].[{2}]
{7}
{6}";

        private const string query_documents_format = @"with MarkForDeletingObjects as(
	select Id, Date, Number 
	from {0} 
	where Deleted=1 AND ID=@Id)
, TotalResult as(
	{1})

select 
    MarkForDeletingObjectId,
    ISNULL(ObjectHolderId, 0) ObjectHolderId,
    TableName,
    SubTableName,
    FieldName,
    ISNULL(RowNumber, 0) RowNumber 
from TotalResult
order by TableName,SubTableName,FieldName,RowNumber,ObjectHolderId";

        private const string query_documents_holders_format = @"select 
   MarkForDeletingObjects.Number MarkForDeletingObjectNumber,
   MarkForDeletingObjects.Id MarkForDeletingObjectId,
   {3}.Id{4} ObjectHolderId,
   '{0}' TableName,
   '{1}' SubTableName,
   '{2}' FieldName,
   {5} as RowNumber from MarkForDeletingObjects
left join {3} on MarkForDeletingObjects.Id = [{3}].[{2}]
{7}
{6}";
        #endregion

        /// <summary>Получить информацию о блокируемых объектах</summary>
        /// <param name="tableName">Имя таблицы удаляемого объекта</param>
        /// <param name="objId">ИД удаляемого объекта</param>
        /// <returns>Информация о блокируемых объектах (список)</returns>
        public static List<DeleteObjectInfo> GetBlockedInfo(string tableName, long objId)
            {
            DeleteMarked deleteMarked = new DeleteMarked();
            return deleteMarked.GetListOfBlockedPlaces(tableName, objId);
            }

        private List<DeleteObjectInfo> GetListOfBlockedPlaces(string objectName, long objId)
            {
            MarkedObjectLinks links = new MarkedObjectLinks(objectName);

            if (links.ObjectReferences.Count != 0)
                {
                DataTable table = getReferedTable(links, objId);
                return getListOfReferedObjects(table);
                }

            //Если объект не используется никакими другими типами, то и блокировать его никто не может
            return new List<DeleteObjectInfo>();
            }

        /// <summary>Получить список ссылающихся объектов</summary>
        /// <param name="table">Таблица данных</param>
        /// <returns>Список ссылающихся объектов</returns>
        private List<DeleteObjectInfo> getListOfReferedObjects(DataTable table)
            {
            List<DeleteObjectInfo> list = new List<DeleteObjectInfo>();

            foreach (DataRow row in table.Rows)
                {
                long blockId = Convert.ToInt64(row["ObjectHolderId"]);

                if (blockId != 0)
                    {
                    long blockRowNumber = Convert.ToInt64(row["RowNumber"]);
                    string blockName = row["TableName"].ToString();
                    string blockSubName = row["SubTableName"].ToString();
                    string blockField = row["FieldName"].ToString();

                    DeleteObjectInfo info = new DeleteObjectInfo(
                        blockName, blockSubName, blockField, blockId,blockRowNumber);
                    list.Add(info);
                    }
                }

            return list;
            }

        /// <summary>Получить таблицу ссылающихся объектов</summary>
        /// <param name="links"></param>
        /// <param name="id"></param>
        /// <returns>Таблица ссылающихся объектов</returns>
        private DataTable getReferedTable(MarkedObjectLinks links, long id)
            {
            string format = string.Empty;
            string format_holders = string.Empty;
            StringBuilder holders = new StringBuilder();

            if (links.DBObjectType == AramisObjectType.Catalog)
                {
                format = query_catalogs_format;
                format_holders = query_catalogs_holders_format;
                }
            else if (links.DBObjectType == AramisObjectType.Document)
                {
                format = query_documents_format;
                format_holders = query_documents_holders_format;
                }

            foreach (ObjectReference objRef in links.ObjectReferences)
                {
                string dBSubtableName = string.Concat("Sub", objRef.TableName, objRef.SubTableName);
                string additionalString = string.Empty;

                if (objRef.CommonSubtableName != string.Empty)
                    {
                    additionalString = string.Concat(
                        "where ",
                        "Common",
                        objRef.CommonSubtableName,
                        ".Guid = '",
                        SystemConfiguration.DBConfigurationTree[objRef.TableName].GUID.ToString(),
                        "'");
                    }

                string holder = String.Format(
                    format_holders,
                    objRef.TableName,
                    objRef.SubTableName,
                    objRef.FieldName,
                    objRef.SubTableName == string.Empty
                        ? objRef.TableName
                        : dBSubtableName,
                    objRef.SubTableName == string.Empty ? string.Empty : "Doc",
                    objRef.SubTableName == string.Empty
                        ? "0"
                        : dBSubtableName + ".LineNumber",
                    additionalString,
                    objRef.SubTableName == string.Empty
                        ? string.Empty
                        : String.Format("left join {0} on {0}.Id = {1}.IdDoc", objRef.TableName, dBSubtableName));
                holders.Append(holders.Length == 0 ? holder : "\r\nunion all\r\n\r\n" + holder);
                }

            Query query = DB.NewQuery(string.Format(format, links.ObjectName, holders));
            query.AddInputParameter("Id", id);
            DataTable table = query.SelectToTable();
            
            if (table == null)
                {
                throw new NullReferenceException("Ошибка виполнения запроса получения данных, о местах в которых есть ссылки на объект!");
                }

            return table;
            }
        }
    }