namespace WMS_client
    {
    using SystemCF;
    using SystemCF.Reflection;
    using SystemCF.Reflection.Emit;

    public class RemoteInteractionProviderCreator<T>
        {
        private System.Type interfaceType;

        public RemoteInteractionProviderCreator()
            {
            interfaceType = typeof(T);
            }

        const string CLASS_NAME = "AramisInteractionProvider";

        public T CreateProvider()
            {
            var typeBuilder = createTypeBuilder();

            var methods = interfaceType.GetMethods();
            foreach (var methodInfo in methods)
                {
                appendMethod(typeBuilder, methodInfo);
                }

            createDefaultConstructor(typeBuilder);
            
            var providerInstance = SystemCF.Activator.CreateInstance(typeBuilder);

            T result = (T)providerInstance;
            return result;
            }

        private void createDefaultConstructor(TypeBuilder typeBuilder)
            {
            var constructorBuilder = typeBuilder.DefineConstructor(System.Reflection.MethodAttributes.Public,
                System.Reflection.CallingConventions.Standard,
                new SystemCF.Type[] { });

            var generator = constructorBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ret);
            }

        private void appendMethod(TypeBuilder typeBuilder, System.Reflection.MethodInfo methodInfo)
            {
            var requaredParameters = methodInfo.GetParameters();
            var methodParameters = new SystemCF.Type[requaredParameters.Length];
            var inputParameters = new System.Collections.Generic.List<Type>();
            var outputParameters = new System.Collections.Generic.List<Type>();

            for (int parameterIndex = 0; parameterIndex < requaredParameters.Length; parameterIndex++)
                {
                var requaredParameterInfo = requaredParameters[parameterIndex];

                if ((requaredParameterInfo.Attributes & System.Reflection.ParameterAttributes.Out) > 0)
                    {
                    var type = requaredParameterInfo.ParameterType.GetElementType();
                    outputParameters.Add(type);
                    methodParameters[parameterIndex] = ((SystemCF.Type)type).MakeByRefType();
                    }
                else
                    {
                    inputParameters.Add(requaredParameterInfo.ParameterType);
                    methodParameters[parameterIndex] = requaredParameterInfo.ParameterType;
                    }
                }

            var methodBuilder = typeBuilder.DefineMethod(methodInfo.Name,
                System.Reflection.MethodAttributes.Public, methodInfo.ReturnType, methodParameters);

            appendMethodBody(methodBuilder, inputParameters, outputParameters);

            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
            }

        private static readonly MethodInfo WMSClient_Current_MethodInfo = typeof(WMSClient).GetMethod("get_Current", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        private static readonly MethodInfo WMSClient_PerformQuery_MethodInfo = typeof(WMSClient).GetMethod("_PerformQueryForInteractionProvider");
        private static readonly MethodInfo ConvertToBoolMethodInfo = typeof(System.Convert).GetMethod("ToBoolean", new System.Type[] { typeof(object) });
        private static readonly MethodInfo ConvertToByteMethodInfo = typeof(System.Convert).GetMethod("ToByte", new System.Type[] { typeof(object) });
        private static readonly MethodInfo ConvertToInt32MethodInfo = typeof(System.Convert).GetMethod("ToInt32", new System.Type[] { typeof(object) });
        private static readonly MethodInfo ConvertToInt64MethodInfo = typeof(System.Convert).GetMethod("ToInt64", new System.Type[] { typeof(object) });
        private static readonly MethodInfo ConvertToDoubleMethodInfo = typeof(System.Convert).GetMethod("ToDouble", new System.Type[] { typeof(object) });
        private static readonly MethodInfo ConvertToDecimalMethodInfo = typeof(System.Convert).GetMethod("ToDecimal", new System.Type[] { typeof(object) });
        private static readonly MethodInfo ConvertToStringMethodInfo = typeof(System.Convert).GetMethod("ToString", new System.Type[] { typeof(object) });
        private static readonly MethodInfo ConvertToDateTimeMethodInfo = typeof(System.Convert).GetMethod("ToDateTime", new System.Type[] { typeof(object) });

        private void appendMethodBody(MethodBuilder methodBuilder, 
            System.Collections.Generic.List<Type> inputParameters,
            System.Collections.Generic.List<Type> outputParameters)
            {
            var generator = methodBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldc_I4, inputParameters.Count);
            generator.Emit(OpCodes.Newarr, typeof(object));
            var sourceParameters = generator.DeclareLocal(typeof(object[]));
            generator.Emit(OpCodes.Stloc, sourceParameters);

            for (int parameterIndex = 0; parameterIndex < inputParameters.Count; parameterIndex++)
                {
                var inputParameterType = inputParameters[parameterIndex];
                generator.Emit(OpCodes.Ldloc, sourceParameters);
                generator.Emit(OpCodes.Ldc_I4, parameterIndex);
                generator.Emit(OpCodes.Ldarg, parameterIndex + 1);
                generator.Emit(OpCodes.Box, inputParameterType);
                generator.Emit(OpCodes.Stelem_Ref);
                }

            var success = generator.DeclareLocal(typeof(bool));
            var functionResult = generator.DeclareLocal(typeof(bool));

            generator.Emit(OpCodes.Call, WMSClient_Current_MethodInfo);
            generator.Emit(OpCodes.Ldstr, methodBuilder.Name);
            generator.Emit(OpCodes.Ldloca_S, success);
            generator.Emit(OpCodes.Ldloca_S, functionResult);
            generator.Emit(OpCodes.Ldloc, sourceParameters);

            // WMSClient.Current.PerformQueryForInteractionProvider("<method name>", out success, out functionResult, parameters);
            generator.Emit(OpCodes.Callvirt, WMSClient_PerformQuery_MethodInfo);
            var resultValues = generator.DeclareLocal(typeof(object[]));
            generator.Emit(OpCodes.Stloc, resultValues);

            var notSuccessLabel = generator.DefineLabel();
            generator.Emit(OpCodes.Ldloc, success);
            generator.Emit(OpCodes.Brfalse, notSuccessLabel);

            for (int parameterIndex = 0; parameterIndex < outputParameters.Count; parameterIndex++)
                {
                var parameterType = outputParameters[parameterIndex];
                const int specialValuesCount = 2; // first - is success; second - method result
                generator.Emit(OpCodes.Ldarg, (short)(inputParameters.Count + 1 + parameterIndex));
                generator.Emit(OpCodes.Ldloc, resultValues);
                generator.Emit(OpCodes.Ldc_I4, specialValuesCount + parameterIndex);
                generator.Emit(OpCodes.Ldelem_Ref);

                if (parameterType == typeof(string))
                    {
                    generator.Emit(OpCodes.Call, ConvertToStringMethodInfo);
                    generator.Emit(OpCodes.Stind_Ref);
                    }
                else if (parameterType == typeof(System.DateTime))
                    {
                    generator.Emit(OpCodes.Call, ConvertToDateTimeMethodInfo);
                    generator.Emit(OpCodes.Stobj, typeof(System.DateTime));
                    }
                else if (parameterType == typeof(bool))
                    {
                    generator.Emit(OpCodes.Call, ConvertToBoolMethodInfo);
                    generator.Emit(OpCodes.Stind_I1);
                    }
                else if (parameterType == typeof(byte))
                    {
                    generator.Emit(OpCodes.Call, ConvertToByteMethodInfo);
                    generator.Emit(OpCodes.Stind_I1);
                    }
                else if (parameterType == typeof(int))
                    {
                    generator.Emit(OpCodes.Call, ConvertToInt32MethodInfo);
                    generator.Emit(OpCodes.Stind_I4);
                    }
                else if (parameterType == typeof(long))
                    {
                    generator.Emit(OpCodes.Call, ConvertToInt64MethodInfo);
                    generator.Emit(OpCodes.Stind_I8);
                    }
                else if (parameterType == typeof(double))
                    {
                    generator.Emit(OpCodes.Call, ConvertToDoubleMethodInfo);
                    generator.Emit(OpCodes.Stind_R8);
                    }
                else if (parameterType == typeof(decimal))
                    {
                    generator.Emit(OpCodes.Call, ConvertToDecimalMethodInfo);
                    generator.Emit(OpCodes.Stobj, typeof(decimal));
                    }
                else if (parameterType == typeof(System.Data.DataTable))
                    {
                    generator.Emit(OpCodes.Isinst, typeof(System.Data.DataTable));
                    generator.Emit(OpCodes.Stind_Ref);
                    }
                }

            generator.Emit(OpCodes.Ldloc, functionResult);
            generator.Emit(OpCodes.Ret);

            // if (not success) { init output parameters with default values and return false; }
            generator.MarkLabel(notSuccessLabel);

            for (int parameterIndex = 0; parameterIndex < outputParameters.Count; parameterIndex++)
                {
                var parameterType = outputParameters[parameterIndex];
                generator.Emit(OpCodes.Ldarg, inputParameters.Count + 1 + parameterIndex);

                if (parameterType == typeof(string))
                    {
                    generator.Emit(OpCodes.Ldstr, string.Empty);
                    generator.Emit(OpCodes.Stind_Ref);
                    }
                else if (parameterType == typeof(System.DateTime))
                    {
                    generator.Emit(OpCodes.Ldsfld, MinDateTimeValueFieldInfo);
                    generator.Emit(OpCodes.Stobj, typeof(System.DateTime));
                    }
                else if (parameterType == typeof(bool) || parameterType == typeof(byte))
                    {
                    generator.Emit(OpCodes.Ldc_I4_0);
                    generator.Emit(OpCodes.Stind_I1);
                    }
                else if (parameterType == typeof(int))
                    {
                    generator.Emit(OpCodes.Ldc_I4_0);
                    generator.Emit(OpCodes.Stind_I4);
                    }
                else if (parameterType == typeof(long))
                    {
                    generator.Emit(OpCodes.Ldc_I4_0);
                    generator.Emit(OpCodes.Conv_I8);
                    generator.Emit(OpCodes.Stind_I8);
                    }
                else if (parameterType == typeof(double))
                    {
                    generator.Emit(OpCodes.Ldc_R8, 0.0);
                    generator.Emit(OpCodes.Stind_R8);
                    }
                else if (parameterType == typeof(decimal))
                    {
                    generator.Emit(OpCodes.Ldc_I4_0);
                    generator.Emit(OpCodes.Newobj, decimalConstructorMethodInfo);
                    generator.Emit(OpCodes.Stobj, typeof(decimal));
                    }
                else if (parameterType == typeof(System.Data.DataTable))
                    {
                    generator.Emit(OpCodes.Ldnull);
                    generator.Emit(OpCodes.Stind_Ref);
                    }
                }

            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Ret);
            }

        private System.Reflection.FieldInfo MinDateTimeValueFieldInfo = typeof(System.DateTime).GetField("MinValue");
        private System.Reflection.ConstructorInfo decimalConstructorMethodInfo = typeof(decimal).GetConstructor(new System.Type[] { typeof(int) });
        private AssemblyBuilder assemBuilder;

        private TypeBuilder createTypeBuilder()
            {
            var appDomain = SystemCF.AppDomain.CurrentDomain;
            var aname = new System.Reflection.AssemblyName() { Name = CLASS_NAME };

            assemBuilder = appDomain.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
            ModuleBuilder modBuilder = assemBuilder.DefineDynamicModule(CLASS_NAME + "Module", CLASS_NAME + ".dll");

            var typeBuilder = modBuilder.DefineType(CLASS_NAME, System.Reflection.TypeAttributes.Public, null, new SystemCF.Type[] { typeof(T) });
            return typeBuilder;
            }
        }
    }
