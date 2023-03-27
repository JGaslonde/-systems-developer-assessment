using NetC.JuniorDeveloperExam.Web.Interfaces;
using NetC.JuniorDeveloperExam.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.Services
{
    public class BlogService : IBlogService
    {
        private string blogsJsonPath;

        public BlogService()
        {
            blogsJsonPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BlogsPath"]);

        }
        public Blog Get(int id)
        {
            var blog = GetAll().BlogPosts.SingleOrDefault(x => x.Id == id);

            if (blog == null)
                return null;

            return blog;
        }

        public Blog SaveComment(Comment comment, int blogId, Guid? parentId)
        {
            comment.Date= DateTime.Now;
            comment.Id = Guid.NewGuid();
            comment.ParentCommentId = parentId;

            var blogs = GetAll();

            var blog = blogs.BlogPosts.Where(x => x.Id == blogId).Single();

            if (parentId == null)
                blog.Comments.Add(comment);
            else
            {
                var parentComment = FindById(blog.Comments, parentId.Value);
                if (parentComment == null)
                {
                    Console.WriteLine("Could not find parent comment");
                    return null;
                }
                if (parentComment.Comments == null)
                    parentComment.Comments = new List<Comment>();

                parentComment.Comments.Add(comment);
   
            }
                

            string json = JsonConvert.SerializeObject(blogs);

            try
            {
                File.WriteAllText(blogsJsonPath, json);
                return blog;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public RootObject GetAll()
        {
            using (StreamReader file = File.OpenText(blogsJsonPath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                return JToken.ReadFrom(reader).ToObject<RootObject>();
            }
        }

        

        public Comment FindById(List<Comment> comments, Guid id)
        {
            foreach(var comment in comments){
                if (comment.Id == id)
                {
                    return comment;
                }

                if (comment.Comments != null && comment.Comments.Count() > 0)
                {
                    Comment found = FindById(comment.Comments, id);
                    if (found != null)
                    {
                        return found;
                    }
                }
                    
            }

            return null;
        }

        public void GenerateCommentIds()
        {
            string json;
            using (StreamReader file = File.OpenText(blogsJsonPath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                var temp = JToken.ReadFrom(reader).ToObject<RootObject>();

                foreach (var blog in temp.BlogPosts)
                {
                    if (blog.Comments != null && blog.Comments.Count() > 0)
                        foreach (var comment in blog.Comments)
                            LoopThroughComments(comment, null, 0);
                }

                json = JsonConvert.SerializeObject(temp);

            }

            try
            {
                File.WriteAllText(blogsJsonPath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void LoopThroughComments(Comment comment, Guid? parentId, int depth)
        {
            if (comment.Id == null || comment.Id == Guid.Empty)
                comment.Id = Guid.NewGuid();
            
            if (comment.ParentCommentId != null || comment.ParentCommentId != Guid.Empty)
                comment.ParentCommentId = parentId;
            
                

            if (comment.Comments != null && comment.Comments.Count() > 0)
                foreach (var child in comment.Comments)
                {
                    LoopThroughComments(child, child.Id, depth + 1);
                }
        }
    }
}