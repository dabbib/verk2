using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using h37.Models.Entities;
using h37.Models.ViewModel;

using h37.Services;
namespace vln2Project.Tests.servicesTest
{
    [TestClass]
    public class projectsServicestest
    {
        [TestMethod]
        public void numberOfFilesInProjects()
        {
            //Arrange
            ProjectsServices numberOfFiles = new ProjectsServices();
            //Act
            int result = numberOfFiles.getNumberOfFilesInProject(4);
            //Assert
            Assert.AreEqual();
        }

        [TestMethod]
        public void createproject()
        {
            //Arrange
            ProjectsServices create = new ProjectsServices();
            //Act
            string name;
            string userid;
            projectype type;
            var result = create.createProject(name, userid, type);
            //Assert
            
        }
        [TestMethod]
        public void deleteproject()
        {
            //Arrange
            ProjectsServices delete = new ProjectsServices();
            //Act
            string userid;
            var result = create.createProject(userid);
            //Assert


        }



    }
}
