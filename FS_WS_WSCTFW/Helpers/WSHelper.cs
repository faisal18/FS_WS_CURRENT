using System;
using System.Collections.Generic;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Xml.Schema;
using Microsoft.CSharp;
using System.Reflection;

namespace FS_WS_WSCTFW.Helpers
{
    class WSHelper
    {


        #region WSCaller

        public static string CallWS(string wsdl, string WSRequest, string methodname, object[] paramsArray)
        {
            try
            {
                string WSResponse = WSRequest;


                //         getProxyBindings(GetWSProxy(wsdl));


                MethodInfo[] myMethods = getAllMethodsProxy(GetWSProxy(wsdl));

                foreach (var item in myMethods)
                {
                 
                    if (item.IsSpecialName != true)
                    {
              //         Logger.Info(item.ReflectedType.get);
               //         Logger(((System.Reflection.TypeInfo)((System.Reflection.RuntimeMethodInfo)item).ReflectedType).DeclaredMethods());
                        Logger.Info(item.Name);
                        Logger.Info(item.GetParameters().Length);
                        Logger.Info(item.MethodImplementationFlags.ToString());
                    }
                }

                return DynamicServiceReference(wsdl, WSRequest, methodname, paramsArray);



            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return null;
            }


        }

        public static string DynamicServiceReference(string dWSDL, string wRequest, string methodname, object[] paramsArray)
        {


            try
            {

                ServiceDescription description = getServiceDescription(dWSDL);
                if (description == null)
                {
                    return "Error parsing WSDL, Please check Endpoint address or Contact Administrator ";
                }
                //  Initialize a service description importer.
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.ProtocolName = "Soap12";  // Use SOAP 1.2.
                importer.AddServiceDescription(description, null, null);

                // Report on the service descriptions.

                Logger.Info("Importing service descriptions with  associated schemas.--- " + importer.ServiceDescriptions.Count + " ----  " + importer.Schemas.Count);

                // Generate a proxy client.
                importer.Style = ServiceDescriptionImportStyle.Client;

                // Generate properties to represent primitive values.
                importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;

                // Initialize a Code-DOM tree into which we will import the service.
                CodeNamespace nmspace = new CodeNamespace();
                CodeCompileUnit unit = new CodeCompileUnit();
                unit.Namespaces.Add(nmspace);

                // Import the service into the Code-DOM tree. This creates proxy code
                // that uses the service.
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);

                if (warning == 0)
                {
                    // Generate and print the proxy code in C#.


                    //       return  InvoderWS(unit, wRequest, "FahrenheitToCelsius",new object[] { "55" );
                    //    object ObjProxy =  GetWSProxy(unit, wRequest, methodname, paramsArray);

                    return InvokerWS(unit, wRequest, methodname, paramsArray);


                }
                else
                {
                    // Print an error message.
                    //       Console.WriteLine(warning);
                    Logger.Error(" Error Importing WSDL Information  - - - - -       " + warning.ToString());
                    return null;
                }
            }
            catch (Exception ex)
            {

                Logger.Error(" Error Generating Dynamic Service Reference using provided WSDL Information  - - - - -       " + ex);
                return null;
            }


        }

        public static string InvokerWS(CodeCompileUnit compileunit, string wRequst, string methodname, object[] paramsArray)
        {
            try
            {
                CodeDomProvider compiler = new CSharpCodeProvider();
                string[] references = new[] { "System.Web.Services.dll", "System.Xml.dll" };
                CompilerParameters parameters = new CompilerParameters(references);
                CompilerResults results = compiler.CompileAssemblyFromDom(parameters, compileunit);

                Type[] assemblyTypes = results.CompiledAssembly.GetExportedTypes();
                object webServiceProxy = Activator.CreateInstance(assemblyTypes[0]);


                //System.Reflection.MethodInfo methodInfo = webServiceProxy.GetType().GetMethod("GetNewTransactions");
                //var result = methodInfo.Invoke(webServiceProxy, new object[] { "Thospital", "Thospital@123" });


                System.Reflection.MethodInfo methodInfo = webServiceProxy.GetType().GetMethod(methodname);
                object result = null;
                if (methodInfo != null)
                {

                    System.Reflection.ParameterInfo[] paramss = methodInfo.GetParameters();


                    if (paramsArray.Length != 0 && paramsArray.Length == paramss.Length && paramss.Length != 0)
                    {
                        result = methodInfo.Invoke(webServiceProxy, paramsArray);
                    }
                    else
                    if (paramss.Length == 0 && paramsArray.Length == 0)
                    {
                        //This works fine
                        result = methodInfo.Invoke(webServiceProxy, null);
                    }
                    else
                     if (paramsArray.Length != 0 && paramsArray.Length != paramss.Length && paramss.Length != 0)
                    {
                        //      object[] parametersArray = new object[] { "55" };
                        object[] parametersArray = syncParameters(paramss, paramsArray);

                        //The invoke does NOT work it throws "Object does not match target type"             
                        result = methodInfo.Invoke(webServiceProxy, parametersArray);

                    }
                }






                //     var result1 = methodInfo.Invoke(webServiceProxy, new object[] { wRequst });
                return result.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        public static void GenerateCode(CodeDomProvider provider, CodeCompileUnit compileunit)
        {
            try
            {
                // Build the source file name with the appropriate
                // language extension.
                String sourceFile;
                if (provider.FileExtension[0] == '.')
                {
                    sourceFile = "TestGraph" + provider.FileExtension;
                }
                else
                {
                    sourceFile = "TestGraph." + provider.FileExtension;
                }

                // Create an IndentedTextWriter, constructed with
                // a StreamWriter to the source file.
                //       MemoryStream MS = new MemoryStream();

                IndentedTextWriter tw = new IndentedTextWriter(new System.IO.StreamWriter("c:\\tmp\\" + sourceFile, false), "    ");
                // Generate source code using the code generator.
                provider.GenerateCodeFromCompileUnit(compileunit, tw, new CodeGeneratorOptions());
                //       provider.GenerateCodeFromCompileUnit(compileunit, MS, new CodeGeneratorOptions());

                provider.GenerateCodeFromCompileUnit(compileunit, Console.Out, new CodeGeneratorOptions());
                // Close the output file.
                Logger.Info(sourceFile.ToString());
                tw.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion

        #region DynamicProxy


        public static object GetWSProxy(string dWSDL)
        {



            try
            {
                
                ServiceDescription description = getServiceDescription(dWSDL);
                if (description == null)
                {
                    return "Error parsing WSDL, Please check Endpoint address or Contact Administrator ";
                }

                //  Initialize a service description importer.
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                //     importer.ProtocolName = "Soap12";  // Use SOAP 1.2.
                importer.AddServiceDescription(description, null, null);

                // Report on the service descriptions.

                Logger.Info("Importing service descriptions with  associated schemas.--- " + importer.ServiceDescriptions.Count + " ----  " + importer.Schemas.Count);

                // Generate a proxy client.
                importer.Style = ServiceDescriptionImportStyle.Client;

                // Generate properties to represent primitive values.
                importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;

                // Initialize a Code-DOM tree into which we will import the service.
                CodeNamespace nmspace = new CodeNamespace();
                CodeCompileUnit unit = new CodeCompileUnit();
                unit.Namespaces.Add(nmspace);

                // Import the service into the Code-DOM tree. This creates proxy code
                // that uses the service.
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);

                if (warning == 0)
                {
                    CodeDomProvider compiler = new CSharpCodeProvider();
                    string[] references = new[] { "System.Web.Services.dll", "System.Xml.dll" };
                    CompilerParameters parameters = new CompilerParameters(references);
                    CompilerResults results = compiler.CompileAssemblyFromDom(parameters, unit);

                    Type[] assemblyTypes = results.CompiledAssembly.GetExportedTypes();
                    object webServiceProxy = Activator.CreateInstance(assemblyTypes[0]);


                    return webServiceProxy;

                }
                else
                {
                    // Print an error message.
                    //       Console.WriteLine(warning);
                    Logger.Error(" Error Importing WSDL Information  - - - - -       " + warning.ToString());
                    return null;
                }
            }
            catch (Exception ex)
            {

                Logger.Error(" Error Generating Dynamic Service Reference using provided WSDL Information  - - - - -       " + ex);
                return null;
            }










        }


        #endregion

        #region WSDetails

        public static bool AddWSDetails(string wsdl, int envID, string userID)
        {
            try
            {

                ServiceDescription description = getServiceDescription(wsdl);



                // Fetchin all the bindings for the WS
                List<string> bindings = getBindings(description);


                foreach (var item in bindings)
                {
                    // Fetchin all the Methods for the WS
                    List<string> methods = getMethods(description, item);

                    foreach (var method in methods)
                    {
                        // Fetchin all SOAPACtion for the Methods
                        string soapaction = getSOAPActions(description, item, methods.IndexOf(method));

                        Models.WSMethods MethodDetails = new Models.WSMethods();

                        MethodDetails.MethodName = method;
                        MethodDetails.SOAPAction = soapaction;
                        MethodDetails.Binding = item;
                        MethodDetails.WSEnvID = envID;
                        MethodDetails.isActive = true;
                        MethodDetails.isDeleted = false;
                        MethodDetails.CreatedDate = DateTime.Now;
                        MethodDetails.CreatedBy = userID;

                        int MethodID = Models.WSMethods.AddMethodDetails(MethodDetails);
                        if (MethodID <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            // Fetchin all the Parameters for the Methods
                            // All Input Parameters
                            Dictionary<string, string> param = getMethodParameters(description, item, method);

                            foreach (var parameter in param)
                            {

                                Models.WSMethodParameters ParameterDetails = new Models.WSMethodParameters();


                                ParameterDetails.ParameterName = parameter.Key;
                                ParameterDetails.ParameterDataType = parameter.Value;
                                ParameterDetails.ParameterType = "Input";
                                ParameterDetails.WSEnvID = envID;
                                ParameterDetails.WSMethodID = MethodID;

                                ParameterDetails.CreatedBy = userID;
                                ParameterDetails.CreatedDate = DateTime.Now;
                                ParameterDetails.isActive = true;
                                ParameterDetails.isDeleted = false;

                                int parameterID = Models.WSMethodParameters.AddMethodParameters(ParameterDetails);
                                if (parameterID > 0)
                                {

                                }
                                else
                                {
                                    return false;

                                }

                            }
                            //For output parameters
                            Dictionary<string, string> outParam = getMethodParameters(description, item, method + "Response");

                            foreach (var parameter in outParam)
                            {

                                Models.WSMethodParameters ParameterDetails = new Models.WSMethodParameters();


                                ParameterDetails.ParameterName = parameter.Key;
                                ParameterDetails.ParameterDataType = parameter.Value;
                                ParameterDetails.ParameterType = "Output";
                                ParameterDetails.WSEnvID = envID;
                                ParameterDetails.WSMethodID = MethodID;

                                ParameterDetails.CreatedBy = userID;
                                ParameterDetails.CreatedDate = DateTime.Now;
                                ParameterDetails.isActive = true;
                                ParameterDetails.isDeleted = false;

                                int parameterID = Models.WSMethodParameters.AddMethodParameters(ParameterDetails);
                                if (parameterID > 0)
                                {

                                }
                                else
                                {
                                    return false;

                                }



                            }


                        }
                    }


                }








                return true;

            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return false;
            }


        }




        #endregion

        #region WSHelpers

        public static List<string> getBindings(ServiceDescription WSdescription)
        {
            try
            {

                List<string> BindingName = new List<string>();

                for (int i = 0; i < WSdescription.Bindings.Count; i++)
                {

                    //   Logger.Info("Types - Schemas -   " + description.Types.Schemas[0].Items[i]..Count.ToString());
                    BindingName.Add(WSdescription.Bindings[i].Name.ToString());
                    Logger.Info("BInding -    " + WSdescription.Bindings[i].Name.ToString());

                }


                return BindingName;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }


        public static List<string> getProxyBindings(Object WSProxyObject)
        {
            try
            {
                
                List<string> BindingName = new List<string>();

                //     Logger.Info( WSProxyObject.GetType().GetArrayRank().ToString());
                //Logger.Info(WSProxyObject.GetType().GetInterfaces().ToString());
                //Logger.Info(WSProxyObject.GetType().GetMembers().ToString());

                //Logger.Info(WSProxyObject.GetType().GetMethods().ToString());


                //Logger.Info(WSProxyObject.GetType().GetProperties().ToString());
                //Logger.Info(WSProxyObject.GetType().GetFields().ToString());
                //Logger.Info(WSProxyObject.GetType().GetEvents().ToString());

                //     OperationBinding OB = WSProxyObject.GetType().GetMethod()

                foreach (var item in WSProxyObject.GetType().GetMethods())
                {

                    Logger.Info("-------------------------Methods---------------------------------");
                    Logger.Info("----------------" + item.Name + "--------------------------------");
                    System.Reflection.MethodInfo MI = WSProxyObject.GetType().GetMethod(item.Name);
                    //  Logger.Info(MI.IsGenericMethod.ToString());
                    //  Logger.Info(MI.IsGenericMethodDefinition.ToString());
                    ////  Logger.Info(MI.ReturnParameter.Name.ToString());
                    //  Logger.Info(MI.ReturnParameter.ToString());
                    //  Logger.Info(MI.GetType().ToString());
                    //       Logger.Info(((System.Web.Services.Protocols.SoapHttpClientProtocol)WSProxyObject).clientType.binding.Name);

                    MI.GetBaseDefinition();

                    Logger.Info("-------------------------Parameters---------------------------------");
                    Logger.Info(MI.GetParameters().Length);
                    foreach (var param in MI.GetParameters())
                    {
                        Logger.Info("----------------" + param.Name + "--------------------------------");


                        //Logger.Info(param.IsOut);
                        //Logger.Info(param.IsIn);
                        //Logger.Info(param.GetType());
                        //Logger.Info(param.ParameterType);
                        System.Reflection.ParameterInfo PI = param;

                        Logger.Info(PI.ParameterType.IsByRef);

                    }


                }

                //foreach (var item in WSProxyObject.GetType().GetFields())
                //{

                //    Logger.Info("-------------------------Fields---------------------------------");

                //    System.Reflection.FieldInfo FI = WSProxyObject.GetType().GetField(item.Name);
                //    Logger.Info(FI.FieldType.IsByRef.ToString());




                //}


                return BindingName;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }




        public static Hashtable getBindingsHash(ServiceDescription WSdescription)
        {
            try
            {

                Hashtable BindingName = new Hashtable();

                for (int i = 0; i < WSdescription.Bindings.Count; i++)
                {

                    //   Logger.Info("Types - Schemas -   " + description.Types.Schemas[0].Items[i]..Count.ToString());
                    //BindingName.Add(WSdescription.Bindings[i].Name.ToString(), WSdescription.Services[0]);

                    //BindingName.Add(WSdescription.Bindings[i].Name.ToString());
                    //Logger.Info("BInding -    " + WSdescription.Bindings[i].Name.ToString());

                }


                return BindingName;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        public static MethodInfo[] getAllMethodsProxy(Object WSProxyObject)
        {
            try
            {
                //      MethodInfo[] MethodsInfoDeclared = WSProxyObject.GetType().GetTypeInfo().GetDeclaredMethods();
                MethodInfo[] MethodsInfo = WSProxyObject.GetType().GetMethods();

         
                return MethodsInfo;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        public static List<string> getMethods(ServiceDescription WSdescription, string BindingName)
        {
            try
            {

                List<string> operationName = new List<string>();

                for (int i = 0; i < WSdescription.Bindings[BindingName].Operations.Count; i++)
                {

                    //   Logger.Info("Types - Schemas -   " + description.Types.Schemas[0].Items[i]..Count.ToString());
                    operationName.Add(WSdescription.Bindings[BindingName].Operations[i].Name.ToString());
                    Logger.Info("BInding  - Operations -   " + WSdescription.Bindings[BindingName].Operations[i].Name.ToString());

                }


                return operationName;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        public static Hashtable getMethodsHash(ServiceDescription WSdescription, string BindingName)
        {
            try
            {

                Hashtable operationName = new Hashtable();

                for (int i = 0; i < WSdescription.Bindings[BindingName].Operations.Count; i++)
                {

                    //   Logger.Info("Types - Schemas -   " + description.Types.Schemas[0].Items[i]..Count.ToString());
                    //operationName.Add(WSdescription.Bindings[BindingName].Operations[i].Name.ToString());
                    //Logger.Info("BInding  - Operations -   " + WSdescription.Bindings[BindingName].Operations[i].Name.ToString());

                }


                return operationName;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }



        public static string getSOAPActions(ServiceDescription WSdescription, string BindingName, int MethodNameIndex)
        {
            try
            {

                string SoapAction = "";

                SoapOperationBinding Stt = (SoapOperationBinding)WSdescription.Bindings[BindingName].Operations[MethodNameIndex].Extensions[0];
                string test = WSdescription.Bindings[BindingName].Operations[MethodNameIndex].Extensions[0].ToString();
                //   string test = description.Bindings[0].Operations[0].Extensions[0].ToString();
                SoapAction = Stt.SoapAction.ToString();





                return SoapAction;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WSdescription"></param>
        /// <param name="BindingName"></param>
        /// <param name="MethodName"></param>
        /// <returns> Dictionary object containing Parameter Name and TYpe</returns>
        /// 
        public static Dictionary<string, string> getMethodParameters(ServiceDescription WSdescription, string BindingName, string MethodName)
        {
            try
            {
          //      SoapOperationBinding Stt = WSdescription.Bindings[BindingName].Operations[0].Extensions[0];

                Dictionary<string, string> Params = new Dictionary<string, string>();

                XmlSchema xmlSchema = WSdescription.Types.Schemas[0];
                foreach (object item in xmlSchema.Items)
                {
                    XmlSchemaElement schemaElement = item as XmlSchemaElement;
                    XmlSchemaComplexType complexType = item as XmlSchemaComplexType;

                    if (schemaElement != null && schemaElement.Name == MethodName)
                    {
                        Logger.Info("Schema Element: " + schemaElement.Name);

                        XmlSchemaType schemaType = schemaElement.SchemaType;
                        XmlSchemaComplexType schemaComplexType = schemaType as XmlSchemaComplexType;

                        if (schemaComplexType != null)
                        {
                            XmlSchemaParticle particle = schemaComplexType.Particle;
                            XmlSchemaSequence sequence =
                                particle as XmlSchemaSequence;
                            if (sequence != null)
                            {
                                foreach (XmlSchemaElement childElement in sequence.Items)
                                {
                                    Logger.Info("    Element/Type: " + childElement.Name + "/" + childElement.SchemaTypeName.Name);
                                    Params.Add(childElement.Name, childElement.SchemaTypeName.Name);
                                }
                            }
                        }
                    }
                    else if (complexType != null)
                    {
                        Logger.Info("Complex Type: " + complexType.Name);
                        //         OutputElements(complexType.Particle);
                    }



                }
                return Params;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WSdescription"></param>
        /// <returns>Dictionary object containing parameter name and type</returns>
        public static Dictionary<string, string> getAllParameters(ServiceDescription WSdescription)
        {
            try
            {

                Dictionary<string, string> Params = new Dictionary<string, string>();

                XmlSchema xmlSchema = WSdescription.Types.Schemas[0];
                foreach (object item in xmlSchema.Items)
                {
                    XmlSchemaElement schemaElement = item as XmlSchemaElement;
                    XmlSchemaComplexType complexType = item as XmlSchemaComplexType;

                    if (schemaElement != null)
                    {
                        Logger.Info("Schema Element: " + schemaElement.Name);

                        XmlSchemaType schemaType = schemaElement.SchemaType;
                        XmlSchemaComplexType schemaComplexType = schemaType as XmlSchemaComplexType;

                        if (schemaComplexType != null)
                        {
                            XmlSchemaParticle particle = schemaComplexType.Particle;
                            XmlSchemaSequence sequence =
                                particle as XmlSchemaSequence;
                            if (sequence != null)
                            {
                                foreach (XmlSchemaElement childElement in sequence.Items)
                                {
                                    Logger.Info("    Element/Type: " + childElement.Name + "/" + childElement.SchemaTypeName.Name);
                                    Params.Add(childElement.Name, childElement.SchemaTypeName.Name);
                                }
                            }
                        }
                    }
                    else if (complexType != null)
                    {
                        Logger.Info("Complex Type: " + complexType.Name);
                        //         OutputElements(complexType.Particle);
                    }



                }
                return Params;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }


        public static ServiceDescription getServiceDescription(string wsdlpath)
        {
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                System.IO.Stream stream =
                       client.OpenRead(wsdlpath);
                // Get a WSDL file describing a service.
                ServiceDescription description = ServiceDescription.Read(stream);
                return description;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return null;
            }


        }

        public static object[] syncParameters(object[] reflectionParams, object[] DbParam)
        {
            try
            {

                object[] resultParam = new object[reflectionParams.Length];

                if (reflectionParams.Length < DbParam.Length)
                {
                    for (int i = 0; i < reflectionParams.Length; i++)
                    {
                        resultParam[i] = DbParam[i];
                    }
                }
                else
                    if (reflectionParams.Length > DbParam.Length)
                {
                    for (int i = 0; i < DbParam.Length; i++)
                    {
                        resultParam[i] = DbParam[i];

                    }
                    for (int i = 0; i < resultParam.Length; i++)
                    {

                        if (resultParam[i] == null)
                            resultParam[i] = "";
                    }




                }

                return resultParam;
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                return null;

            }



        }

        #endregion



    }
}
