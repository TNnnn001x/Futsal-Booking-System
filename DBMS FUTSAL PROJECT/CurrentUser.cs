using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS_FUTSAL_PROJECT
{
    internal class CurrentUser
    {
        public static int EmpID { get; set; } // เก็บ ID ของพนักงาน
        public static string EmpPosition { get; set; } // เก็บตำแหน่งของพนักงาน
        public static string Role { get; set; } // เก็บบทบาทของผู้ใช้ (เช่น Employee หรือ Student)

        // ข้อมูลสำหรับนักศึกษา
        public static Int64 StudentID { get; set; } // เก็บ ID ของนักศึกษา
        public static string StudentName { get; set; } // เก็บชื่อของนักศึกษา
        public static string StudentFaculty { get; set; } // เก็บคณะของนักศึกษา
        public static string StudentMajor { get; set; } // เก็บสาขาของนักศึกษา
    }
}
