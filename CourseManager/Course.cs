namespace CourseManager {
    public class Course
    {

        private string dept;
        private string code;
        private string title;

        public Course(string dept, string code, string title)
        {
            this.dept = dept;
            this.code = code;
            this.title = title;
        }
        public string GetCourseCode()
        {
            return code;
        }
        public string GetCourseTitle()
        {
            return title;
        }
    }
}//end namespace System