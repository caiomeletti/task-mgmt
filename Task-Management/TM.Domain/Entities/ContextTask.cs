﻿namespace TM.Domain.Entities
{
    public class ContextTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public byte Priority { get; set; }
        public byte Status { get; set; }
        public int ProjectId { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UserId { get; set; }

        public ContextTask()
        {
            Title = string.Empty;
            Description = string.Empty;
            UpdateAt = DateTime.Now;
        }
    }
}