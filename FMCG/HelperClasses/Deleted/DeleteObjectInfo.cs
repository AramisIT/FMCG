using Aramis.Core;
using Aramis.SystemConfigurations;

namespace AtosFMCG
    {
    /// <summary>Информацио о удаляемом объекте</summary>
    public class DeleteObjectInfo
        {
        /// <summary>Имя таблицы</summary>
        public InformationName<string> TableName { get; set; }
        /// <summary>Имя подтаблицы</summary>
        public InformationName<string> SubTableName { get; set; }
        /// <summary>Имя поля</summary>
        public InformationName<string> FieldName { get; set; }
        /// <summary>ИД</summary>
        public long Id { get; set; }
        /// <summary>Номер строки в подтаблице</summary>
        public long LineNumber { get; set; }
        /// <summary>Является ссылкой на подтаблицу</summary>
        public bool IsSubtableLink
            {
            get { return SubTableName.Name != null; }
            }

        /// <summary>Информацио о удаляемом объекте</summary>
        /// <param name="name">Имя таблицы</param>
        /// <param name="subName">Имя подтаблицы</param>
        /// <param name="field">Имя поля</param>
        /// <param name="id">ИД</param>
        /// <param name="row">Номер строки в подтаблице</param>
        public DeleteObjectInfo(string name, string subName, string field, long id, long row)
            {
            DatabaseObjectInfo databaseObjectInfo = SystemConfiguration.DBConfigurationTree[name];
            string tableName = databaseObjectInfo.DBObjectType ==AramisObjectType.Catalog
                                   ? new CatalogsViewer(name, id).ToString()
                                   : new DocumentsViewer(name, id).ToString();
            string subTableName;
            string fieldName;

            if (string.IsNullOrEmpty(subName))
                {
                subTableName = null;
                fieldName = databaseObjectInfo.FieldsDictionary[field].Attr.Description;
                }
            else
                {
                subTableName = databaseObjectInfo.SubTablesDesc[subName];
                fieldName = databaseObjectInfo.InfoSubTables[subName].SubtableFields[field].Attr.Description;
                }

            TableName = new InformationName<string>(name, tableName);
            SubTableName = new InformationName<string>(subName, subTableName);
            FieldName = new InformationName<string>(field, fieldName);
            Id = id;
            LineNumber = row;
            }

        public override string ToString()
            {
            return string.Format("Name: {0}; Sub: {1}; Field: {2}; Id: {3}; Row: {4}",
                                 TableName, 
                                 SubTableName,
                                 FieldName,
                                 Id,
                                 LineNumber);
            }
        }
    }