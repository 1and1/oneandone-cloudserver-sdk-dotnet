using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.Logs
{
    public class LogsResponse
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string start_date;

        public string StartDate
        {
            get { return start_date; }
            set { start_date = value; }
        }
        private string end_date;

        public string EndDate
        {
            get { return end_date; }
            set { end_date = value; }
        }
        private int duration;

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        private Status status;

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }
        private string action;

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private Resource resource;

        public Resource Resource
        {
            get { return resource; }
            set { resource = value; }
        }
        private object details;

        public object Details
        {
            get { return details; }
            set { details = value; }
        }
        private User user;

        public User User
        {
            get { return user; }
            set { user = value; }
        }
        private string cloudpanel_id;

        public string CloudpanelId
        {
            get { return cloudpanel_id; }
            set { cloudpanel_id = value; }
        }
    }

    public class Status
    {
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private int percent;

        public int Percent
        {
            get { return percent; }
            set { percent = value; }
        }
    }

    public class Resource
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class Details
    {
    }

    public class User
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
