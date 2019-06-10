﻿using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Teacher
{
    [Table("TecCourse")]
    public class ETecCourse : ECBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(50)]
        public string LessonCode { get; set; }

        [MaxLength(20)]
        public string CourseName { get; set; }

        [MaxLength(20)]

        public string TecCode { get; set; }

        public CourseScheduleType CourseScheduleType { get; set; }

        public TecCoursingStatus CoursingStatus { get; set; }

        public DateTime CourseDateTime { get; set; }


        public int Day { get; set; }

        public int Lesson { get; set; }
        public double TimeStart { get; set; }
        public double TimeEnd { get; set; }


    }
}