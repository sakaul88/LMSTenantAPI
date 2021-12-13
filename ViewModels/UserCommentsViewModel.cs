﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class UserCommentsViewModel
    {
        public int Id { get; set; }
        public int? FkUserId { get; set; }
        public int? FkCourseId { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual CourseMasterViewModel FkCourse { get; set; }
        public virtual UserMasterViewModel FkUser { get; set; }
    }
}
