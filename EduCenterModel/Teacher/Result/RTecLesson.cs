using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Teacher.Result
{
    public class RTecLesson
    {
        /// <summary>
        /// CourseId
        /// </summary>
        public long Id { get; set; }
        public string TecCode { get; set; }

        public string TimeRange { get; set; }

        public string CourseName { get; set; }

        public string LessonCode { get; set; }

        public int Lesson { get; set; }

        public TecCoursingStatus CoursingStatus { get; set; }
        public string CourseStatusName { get; set; }
    }
}
