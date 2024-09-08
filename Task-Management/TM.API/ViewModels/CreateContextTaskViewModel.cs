﻿using TM.Core.Enum;

namespace TM.API.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateContextTaskViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Priority Priority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CurrentTaskStatus Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
    }
}
