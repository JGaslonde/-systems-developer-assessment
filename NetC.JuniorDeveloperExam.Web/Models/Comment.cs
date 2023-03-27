using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetC.JuniorDeveloperExam.Web.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid? ParentCommentId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Required]
        public string Message { get; set; }
        public List<Comment> Comments { get; set; }
    }
}