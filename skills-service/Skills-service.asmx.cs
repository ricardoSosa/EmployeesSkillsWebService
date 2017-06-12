using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Neo4jClient;

namespace skills_service
{
    /// <summary>
    /// Summary description for Skills_service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Skills_service : System.Web.Services.WebService
    {

        GraphClient graphClient;
        public Skills_service()
        {
            graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "skills");
            graphClient.Connect();
        }

        [WebMethod]
        public string createEmployee(string employeeName)
        {
            string result = "";

            if (employeeName != "")
            {
                if (existsEmployee(employeeName))
                {
                    result = "The employee actually exists.";
                }
                else
                {
                    Employee newEmployee = new Employee(employeeName);
                    graphClient.Cypher
                        .Merge("(emp:Employee {name: {name}})")
                        .OnCreate()
                        .Set("emp = {newEmployee}")
                        .WithParams(new
                        {
                            name = newEmployee.getName(),
                            newEmployee
                        })
                        .ExecuteWithoutResults();
                    result = "The employee has been created.";
                }
            }
            else
            {
                result = "The employee needs a name.";
            }

            return result;
        }

        [WebMethod]
        public string modifyEmployee(string employeeName, string newName)
        {
            string result = "";

            if (employeeName != "" && newName != "")
            {
                if (existsEmployee(employeeName))
                {
                    if (existsEmployee(newName))
                    {
                        result = "Actually exists an employee with that new name.";
                    }
                    else
                    {
                        graphClient.Cypher
                            .Match("(emp:Employee)")
                            .Where("emp.name = {employeeName}")
                            .WithParam("employeeName", employeeName)
                            .Set("emp.name = {newName}")
                            .WithParam("newName", newName)
                            .ExecuteWithoutResults();

                        result = "The employee has been modified.";
                    }
                }
                else
                {
                    result = "The employee doesn't exists.";
                }

            }
            else
            {
                result = "The parameters cannot be empty.";
            }

            return result;
        }

        [WebMethod]
        public string deleteEmployee(string employeeName)
        {
            string result = "";

            if (employeeName != "")
            {
                if (existsEmployee(employeeName))
                {
                    graphClient.Cypher
                        .Match("(emp:Employee)")
                        .Where("emp.name = {employeeName}")
                        .WithParam("employeeName", employeeName)
                        .DetachDelete("emp")
                        .ExecuteWithoutResults();

                    result = "The employee has been deleted.";
                }
                else
                {
                    result = "The employee doesn't exists.";
                }
            }
            else
            {
                result = "The parameters cannot be empty.";
            }

            return result;
        }

        [WebMethod]
        public string createSkill(string skillName)
        {
            string result = "";

            if (skillName != "")
            {
                if (existsSkill(skillName))
                {
                    result = "The skill actually exists.";
                }
                else
                {
                    Skill newSkill = new Skill(skillName);
                    graphClient.Cypher
                        .Merge("(sk:Skill {name: {name}})")
                        .OnCreate()
                        .Set("sk = {newSkill}")
                        .WithParams(new
                        {
                            name = newSkill.getName(),
                            newSkill
                        })
                        .ExecuteWithoutResults();
                    result = "The skill has been created.";
                }
            }
            else
            {
                result = "The skill needs a name.";
            }
            return result;
        }

        [WebMethod]
        public string modifySkill(string skillName, string newName)
        {
            string result = "";

            if (skillName != "" && newName != "")
            {
                if (existsSkill(skillName))
                {
                    if (existsSkill(newName))
                    {
                        result = "Actually exists a skill with that new name.";
                    }
                    else
                    {
                        graphClient.Cypher
                            .Match("(sk:Skill)")
                            .Where("sk.name = {skillName}")
                            .WithParam("skillName", skillName)
                            .Set("sk.name = {newName}")
                            .WithParam("newName", newName)
                            .ExecuteWithoutResults();

                        result = "The skill has been modified.";
                    }
                }
                else
                {
                    result = "The skill doesn't exists.";
                }
            }
            else
            {
                result = "The parameters cannot be empty.";
            }

            return result;
        }

        [WebMethod]
        public string deleteSkill(string skillName)
        {
            string result = "";

            if (skillName != "")
            {
                if (existsSkill(skillName))
                {
                    graphClient.Cypher
                        .Match("(sk:Skill)")
                        .Where("sk.name = {skillName}")
                        .WithParam("skillName", skillName)
                        .DetachDelete("sk")
                        .ExecuteWithoutResults();

                    result = "The skill has been deleted.";
                }
                else
                {
                    result = "The skill doesn't exists.";
                }
            }
            else
            {
                result = "The parameters cannot be empty.";
            }

            return result;
        }

        [WebMethod]
        public string createSkillGroup(string skillGroupName)
        {
            string result = "";

            if (skillGroupName != "")
            {
                if (existsSkillGroup(skillGroupName))
                {
                    result = "The skill actually exists.";
                }
                else
                {
                    SkillGroup newSkillGroup = new SkillGroup(skillGroupName);
                    graphClient.Cypher
                        .Merge("(sk:SkillGroup {name: {name}})")
                        .OnCreate()
                        .Set("sk = {newSkillGroup}")
                        .WithParams(new
                        {
                            name = newSkillGroup.getName(),
                            newSkillGroup
                        })
                        .ExecuteWithoutResults();
                    result = "The skill has been created.";
                }
            }
            else
            {
                result = "The skill needs a name.";
            }
            return result;
        }

        [WebMethod]
        public string modifySkillGroup(string skillGroupName, string newName)
        {
            string result = "";

            if (skillGroupName != "" && newName != "")
            {
                if (existsSkillGroup(skillGroupName))
                {
                    if (existsSkillGroup(newName))
                    {
                        result = "Actually exists a skill group with that new name.";
                    }
                    else
                    {
                        graphClient.Cypher
                            .Match("(sk:SkillGroup)")
                            .Where("sk.name = {skillGroupName}")
                            .WithParam("skillGroupName", skillGroupName)
                            .Set("sk.name = {newName}")
                            .WithParam("newName", newName)
                            .ExecuteWithoutResults();

                        result = "The skill has been modified.";
                    }
                }
                else
                {
                    result = "The skill doesn't exists.";
                }
            }
            else
            {
                result = "The parameters cannot be empty.";
            }

            return result;
        }

        [WebMethod]
        public string deleteSkillGroup(string skillGroupName)
        {
            string result = "";

            if (skillGroupName != "")
            {
                if (existsSkillGroup(skillGroupName))
                {
                    graphClient.Cypher
                        .Match("(sk:SkillGroup)")
                        .Where("sk.name = {skillGroupName}")
                        .WithParam("skillGroupName", skillGroupName)
                        .DetachDelete("sk")
                        .ExecuteWithoutResults();

                    result = "The skill has been deleted.";
                }
                else
                {
                    result = "The skill doesn't exists.";
                }
            }
            else
            {
                result = "The parameters cannot be empty.";
            }

            return result;
        }

        [WebMethod]
        private bool existsEmployee(string employeeName)
        {
            bool exists = false;

            var employee =
                graphClient.Cypher
                .Match("(emp:Employee)")
                .Where("emp.name = {employeeName}")
                .WithParam("employeeName", employeeName)
                .Return(emp => emp.As<Employee>().name)
                .Results.SingleOrDefault();

            if (employee == null)
            {
                exists = false;
            }
            else
            {
                exists = true;
            }

            return exists;
        }

        [WebMethod]
        private bool existsSkill(string skillName)
        {
            bool exists = false;

            var skill =
                graphClient.Cypher
                    .Match("(sk:Skill)")
                    .Where("sk.name = {skillName}")
                    .WithParam("skillName", skillName)
                    .Return(sk => sk.As<Skill>().name)
                    .Results.SingleOrDefault();

            if (skill == null)
            {
                exists = false;
            }
            else
            {
                exists = true;
            }

            return exists;
        }

        [WebMethod]
        private bool existsSkillGroup(string skillGroupName)
        {
            bool exists = false;

            var skillGroup =
                graphClient.Cypher
                .Match("(skgp:SkillGroup)")
                .Where("skgp.name = {skillGroupName}")
                .WithParam("skillGroupName", skillGroupName)
                .Return(skgp => skgp.As<SkillGroup>().name)
                .Results.SingleOrDefault();

            if (skillGroup == null)
            {
                exists = false;
            }
            else
            {
                exists = true;
            }

            return exists;
        }

        [WebMethod]
        public string assignSkillToEmployee(string employeeName, string skillName, string skillLevel)
        {
            string result = "";

            if (employeeName != "" && skillName != "" && skillLevel != "")
            {
                if (skillLevel == "Junior" || skillLevel == "Intermediate" || skillLevel == "Senior" || skillLevel == "Lead")
                {
                    if (existsEmployee(employeeName) && existsSkill(skillName))
                    {
                        if (isEmployeeRelatedToSkill(employeeName, skillName))
                        {
                            modifySkillLevelToEmployee(employeeName, skillName, skillLevel);
                        }
                        else
                        {
                            graphClient.Cypher
                                .Match("(emp:Employee), (sk:Skill)")
                                .Where("emp.name = {employeeName}")
                                .WithParam("employeeName", employeeName)
                                .AndWhere("sk.name = {skillName}")
                                .WithParam("skillName", skillName)
                                .Create("(emp)-[:HAS_EXPERIENCE {level: {skillLevel}}]->(sk)")
                                .WithParam("skillLevel", skillLevel)
                                .ExecuteWithoutResults();
                        }
                        result = "Assigned level";
                    }
                    else
                    {
                        result = "The employee or skill doesn't exist.";
                    }
                }
                else
                {
                    result = "Invalid skill level.";
                }
            }
            else
            {
                result = "The parameters cannot be empty.";
            }

            return result;
        }

        [WebMethod]
        private void modifySkillLevelToEmployee(string employeeName, string skillName, string skillLevel)
        {
            graphClient.Cypher
                .Match("(emp:Employee)-[rel:HAS_EXPERIENCE]->(sk:Skill)")
                .Where("emp.name = {employeeName}")
                .WithParam("employeeName", employeeName)
                .AndWhere("sk.name = {skillName}")
                .WithParam("skillName", skillName)
                .Set("rel.level = {skillLevel}")
                .WithParam("skillLevel", skillLevel)
                .ExecuteWithoutResults();
        }

        [WebMethod]
        private bool isEmployeeRelatedToSkill(string employeeName, string skillName)
        {
            bool isRelated = false;

            var result =
                graphClient.Cypher
                .Match("(emp:Employee)-[rel]->(sk:Skill)")
                .Where("emp.firstName = {employeeName}")
                .WithParam("employeeName", employeeName)
                .AndWhere("sk.name = {skillName}")
                .WithParam("skillName", skillName)
                .Return(rel => new {
                    R = rel.As<RelationshipInstance<object>>()
                }).Results;

            if (result.Count() != 0)
            {
                isRelated = true;
            }

            return isRelated;
        }

        [WebMethod]
        public string relateSkillToSkillGroup(string skillName, string skillGroupName)
        {
            string result = "";

            if (skillName != "" && skillGroupName != "")
            {
                if (existsSkill(skillName) && existsSkillGroup(skillGroupName))
                {
                    if (isSkillRelatedToSkillGroup(skillName, skillGroupName))
                    {
                        result = "The skill is actually related to the skill group.";
                    }
                    else
                    {
                        graphClient.Cypher
                           .Match("(sk:Skill), (skgp:SkillGroup)")
                           .Where("sk.name = {skillName}")
                           .WithParam("skillName", skillName)
                           .AndWhere("skgp.name = {skillGroupName}")
                           .WithParam("skillGroupName", skillGroupName)
                           .Create("(sk)-[:IS_RELATED_TO]->(skgp)")
                           .ExecuteWithoutResults();

                        result = "Assigned skill.";
                    }
                }
                else
                {
                    result = "The skill or skill group doesn't exist.";
                }
            }
            else
            {
                result = "The parameters cannot be empty.";
            }

            return result;
        }

        [WebMethod]
        private bool isSkillRelatedToSkillGroup(string skillName, string skillGroupName)
        {
            bool isRelated = false;

            var result =
                graphClient.Cypher
                .Match("(sk:Skill)-[rel:IS_RELATED_TO]->(skgp:SkillGroup)")
                .Where("sk.name = {skillName}")
                .WithParam("skillName", skillName)
                .AndWhere("skgp.name = {skillGroupName}")
                .WithParam("skillGroupName", skillGroupName)
                .Return(rel => rel.As<RelationshipInstance<object>>())
                .Results;

            if (result.Count() != 0)
            {
                isRelated = true;
            }

            return isRelated;
        }

        [WebMethod]
        public SkillReport getEmployeesBySkill(string skillName)
        {
            var employees =
                graphClient.Cypher
                .Match("(emp:Employee)-[rel:HAS_EXPERIENCE]->(sk:Skill)")
                .Where("sk.name = {skillName}")
                .WithParam("skillName", skillName)
                .ReturnDistinct((emp, rel) => new
                {
                    Emp = emp.As<Employee>().name,
                    Rel = rel.As<SkillExperience>().level
                })
                .Results;

            SkillReport skrp = new SkillReport(skillName);
            foreach (var employee in employees)
            {
                switch (employee.Rel)
                {
                    case "Junior":
                        skrp.junior.Add(employee.Emp);
                        break;
                    case "Intermediate":
                        skrp.intermediate.Add(employee.Emp);
                        break;
                    case "Senior":
                        skrp.senior.Add(employee.Emp);
                        break;
                    case "Lead":
                        skrp.lead.Add(employee.Emp);
                        break;
                }
            }

            return skrp;
        }

        public class Node
        {
            public string name;

            public Node()
            {

            }

            public Node(string name)
            {
                this.name = name;
            }

            public string getName()
            {
                return name;
            }

            public void setName(string name)
            {
                this.name = name;
            }
        }

        public class Employee
        {
            public string name;

            public Employee()
            {

            }

            public Employee(string name)
            {
                this.name = name;
            }

            public string getName()
            {
                return name;
            }

            public void setName(string name)
            {
                this.name = name;
            }
        }

        public class Skill
        {
            public string name;

            public Skill()
            {

            }

            public Skill(string name)
            {
                this.name = name;
            }

            public string getName()
            {
                return name;
            }

            public void setName(string name)
            {
                this.name = name;
            }
        }

        public class SkillGroup
        {
            public string name;

            public SkillGroup()
            {

            }

            public SkillGroup(string name)
            {
                this.name = name;
            }

            public string getName()
            {
                return name;
            }

            public void setName(string name)
            {
                this.name = name;
            }
        }

        public class SkillExperience
        {
            public string level;

            public SkillExperience()
            {

            }

            public SkillExperience(string level)
            {
                this.level = level;
            }

            public string getLevel()
            {
                return level;
            }

            public void setLevel(string level)
            {
                this.level = level;
            }
        }

        public class SkillReport
        {
            public string name;

            public List<string> junior;

            public List<string> intermediate;

            public List<string> senior;

            public List<string> lead;

            public SkillReport()
            {

            }

            public SkillReport(string name)
            {
                this.name = name;
                junior = new List<string>();
                intermediate = new List<string>();
                senior = new List<string>();
                lead = new List<string>();
            }

            public string getName()
            {
                return name;
            }

            public List<string> getJunior()
            {
                return junior;
            }

            public List<string> getIntermediate()
            {
                return intermediate;
            }

            public List<string> getSenior()
            {
                return senior;
            }

            public List<string> getLead()
            {
                return lead;
            }

            public void setName(string name)
            {
                this.name = name;
            }
        }

    }
}
