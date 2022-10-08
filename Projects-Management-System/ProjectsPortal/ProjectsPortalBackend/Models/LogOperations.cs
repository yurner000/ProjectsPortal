using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectsPortalBackend.Models
{
    public class LogOperations
    {
        [Key]
        public int LogID {get; set;}
        public bool LogSituation {get; set;}
        public int UserID {get; set;}
        public string Process {get; set;}
        public DateTime DateTime {get; set;}
        public string UserIp {get; set;}
        public string Statement {get; set;}
    }
}
