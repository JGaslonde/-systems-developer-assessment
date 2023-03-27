using NetC.JuniorDeveloperExam.Web.Models;
using System;

namespace NetC.JuniorDeveloperExam.Web.ViewModels
{
    public class BlogViewModel
    {
        public Blog Blog{ get; set; }
        public Comment NewComment { get; set; }
        public Guid? ParentId { get; set; }
    }
}