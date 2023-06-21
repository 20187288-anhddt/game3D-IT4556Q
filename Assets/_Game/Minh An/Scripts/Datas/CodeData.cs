using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeData : MonoBehaviour
{

}
public static class CodeData_Level_Boss
{
    public static string codeData_Name = "N_b";
    public static string codeData_Level_Capacity = "c";
    public static string codeData_Level_Speed = "s";
    public static string codeData_Level_Price = "p";
    public static string ConvertCode(string string_code)
    {
        string_code = string_code.Replace("DataBoss", codeData_Name);
        string_code = string_code.Replace("level_Capacity_Taget", codeData_Level_Capacity);
        string_code = string_code.Replace("level_Price_Taget", codeData_Level_Price);
        string_code = string_code.Replace("level_Speed_Taget", codeData_Level_Speed);
        return string_code;
    }
    public static string DeConvertCode(string string_code)
    {
        string_code = string_code.Replace(codeData_Name, "DataBoss");
        string_code = string_code.Replace(codeData_Level_Capacity, "level_Capacity_Taget");
        string_code = string_code.Replace(codeData_Level_Price, "level_Price_Taget");
        string_code = string_code.Replace(codeData_Level_Speed, "level_Speed_Taget");
        return string_code;
    }
}
public static class CodeData_Level_Staff
{
    public static string codeData_Name = "N_s";
    public static string codeData_Level_Capacity = "c";
    public static string codeData_Level_Hire = "h";
    public static string codeData_Level_Speed = "s";

    public static string ConvertCode(string string_code)
    {
        string_code = string_code.Replace("DataStaff", codeData_Name);
        string_code = string_code.Replace("level_Capacity_Taget", codeData_Level_Capacity);
        string_code = string_code.Replace("level_Hire_Taget", codeData_Level_Hire);
        string_code = string_code.Replace("level_Speed_Taget", codeData_Level_Speed);
        return string_code;
    }
    public static string DeConvertCode(string string_code)
    {
        string_code = string_code.Replace(codeData_Name, "DataStaff");
        string_code = string_code.Replace(codeData_Level_Capacity, "level_Capacity_Taget");
        string_code = string_code.Replace(codeData_Level_Hire, "level_Hire_Taget");
        string_code = string_code.Replace(codeData_Level_Speed, "level_Speed_Taget");
        return string_code;
    }
}
