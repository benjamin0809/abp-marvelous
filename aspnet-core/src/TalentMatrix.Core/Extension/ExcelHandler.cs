using AutoMapper;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace TalentMatrix.Extension
{
    public class ExcelHandler
    {
        public static void Test()
        {
            Debug.Write(System.Environment.CurrentDirectory);
            Debug.Write(System.AppDomain.CurrentDomain.BaseDirectory);
            Debug.Write(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            string importExcelPath = "E:\\import.xlsx";
            string exportExcelPath = "E:\\export.xlsx";
            IWorkbook workbook = WorkbookFactory.Create(importExcelPath);
            ISheet sheet = workbook.GetSheetAt(0);//获取第一个工作薄
            
            Debug.Write(sheet.PhysicalNumberOfRows);
            for (int i = 0; i < sheet.PhysicalNumberOfRows; i++)
            {
                IRow row = (IRow)sheet.GetRow(i);//获取第一行

                //设置第一行第一列值,更多方法请参考源官方Demo
                row.CreateCell(i).SetCellValue("test");//设置第一行第一列值
                row.Hidden = true;
            }


            //导出excel
            FileStream fs = new FileStream(exportExcelPath, FileMode.Create, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Close();
        }


        public static List<T> ReadExcel<T>(string filePath) where T : new()
        {
            List<T> result = new List<T>();
            Type t = typeof(T);
            MemberInfo[] minfos = t.GetMembers();
            // FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = WorkbookFactory.Create(filePath);
            ISheet sheet = workbook.GetSheetAt(0);//获取第一个工作薄
            if(sheet == null)
            {
                throw new Exception("sheet is null");
            }
            IRow codes_row = sheet.GetRow(0);
                codes_row.Hidden = true;
            for (int i = 1; i < sheet.PhysicalNumberOfRows; i++)
            {
                
                Dictionary<string, object> AttributeDict = new Dictionary<string, object>();
                IRow row = sheet.GetRow(i);
                row.Hidden = true;
                int cellCount = row.PhysicalNumberOfCells;
                for (int j = 0; j < cellCount; j++)
                {
                    AttributeDict.Add(codes_row.GetCell(j).StringCellValue, row.GetCell(j));
                }
               
                result.Add(DicToObject<T> (AttributeDict));
            }
            // fs.Close();
            //ObjectMapper.Map<List<T>>(roles);
            return result;
        }

        /// <summary>
        /// 字典类型转化为对象
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T DicToObject<T>(Dictionary<string, object> dic) where T : new()
        {
            var md = new T();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            foreach (var d in dic)
            {
                var filed = textInfo.ToTitleCase(d.Key);
                try
               {
                    Type propertyType  = md.GetType().GetProperty(filed).PropertyType;
                    var value = d.Value.ToString();
                    md.GetType().GetProperty(filed).SetValue(md, value);
                }
                catch (Exception e)
                {

                }
            }
            return md;
        }

        /// <summary>
        /// Assign parameters to specified objects
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="dic">Fields/values</param>
        /// <returns></returns>
        public static T Assign<T>(Dictionary<string, string> dic) where T : new()
        {
            Type myType = typeof(T);
            T entity = new T();
            var fields = myType.GetProperties();
            string val = string.Empty;
            object obj = null;

            foreach (var field in fields)
            {
                if (!dic.ContainsKey(field.Name))
                    continue;
                val = dic[field.Name];

                object defaultVal;
                if (field.PropertyType.Name.Equals("String"))
                    defaultVal = "";
                else if (field.PropertyType.Name.Equals("Boolean"))
                {
                    defaultVal = false;
                    val = (val.Equals("1") || val.Equals("on")).ToString();
                }
                else if (field.PropertyType.Name.Equals("Decimal"))
                    defaultVal = 0M;
                else
                    defaultVal = 0;

                if (!field.PropertyType.IsGenericType)
                    obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, field.PropertyType);
                else
                {
                    Type genericTypeDefinition = field.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                        obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, Nullable.GetUnderlyingType(field.PropertyType));
                }

                field.SetValue(entity, obj, null);
            }

            return entity;
        }
    }
}

