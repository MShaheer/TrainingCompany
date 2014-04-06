using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TrainingCompany.Controllers
{
    public class CoursesController : ApiController
    {


        static List<course> courses = InitCourses();

        private static List<course> InitCourses()
        {
            var ret = new List<course>();
            ret.Add(new course { id = 0, title = "Web Api" });
            ret.Add(new course { id = 1, title = "Mobile Apps" });
            return ret;
        }



        //POST
        public HttpResponseMessage Post([FromBody]course c) {

            c.id = courses.Count;
            courses.Add(c);

            //return as 201
            var msg = Request.CreateResponse(HttpStatusCode.Created);
            msg.Headers.Location = new Uri(Request.RequestUri + c.id.ToString());
            return msg;
        }


        //PUT (update)
        public void Put(int id, [FromBody]course course)
        {
            var ret = (from c in courses
                       where c.id == id
                       select c).FirstOrDefault();
            ret.title = course.title;
            //return as 201
        }


        //DELETE 
        public void Delete(int id) {
            var ret = (from c in courses
                       where c.id == id
                       select c).FirstOrDefault();
            courses.Remove(ret);
        }

        //GET (select all)
        public IEnumerable<course> Get() {
            return courses;
        }

        //GET (select particular)
        public HttpResponseMessage Get(int id) {

            HttpResponseMessage msg = null;

            var ret = (from c in courses
                       where c.id == id
                       select c).FirstOrDefault();
            //return 401 if null

            if (ret == null) {
                msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course not found");
            }
            else {
                msg = Request.CreateResponse<course>(HttpStatusCode.OK, ret);
            }
            return msg;  
        }

       
    }



    public class course
    {
        public int id;
        public string title;
    }
}
