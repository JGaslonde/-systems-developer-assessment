using NetC.JuniorDeveloperExam.Web.Models;
using System;
using System.Collections.Generic;

namespace NetC.JuniorDeveloperExam.Web.Interfaces
{
    public interface IBlogService
    {
        Blog Get(int id);
        Blog SaveComment(Comment comment, int blogId, Guid? parentId);
        RootObject GetAll();
        Comment FindById(List<Comment> comments, Guid id);
        void GenerateCommentIds();

    }
}