﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.18444
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Ecliptic.Unit.Test.Gherkin
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Annular Velocity")]
    [NUnit.Framework.CategoryAttribute("Story_123")]
    [NUnit.Framework.CategoryAttribute("ECD")]
    public partial class AnnularVelocityFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Annular Velocity.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Annular Velocity", "As a mud engineer I want to know the annular velocity of the drilling string so t" +
                    "hat I can look at ways to drill ahead faster or more efficiently.", ProgrammingLanguage.CSharp, new string[] {
                        "Story_123",
                        "ECD"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 5
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "VG600",
                        "VG300",
                        "VG200",
                        "VG100",
                        "VG6",
                        "VG3"});
            table1.AddRow(new string[] {
                        "120",
                        "75",
                        "45",
                        "22",
                        "5",
                        "3"});
#line 6
testRunner.Given("that viscometer readings are as follows:", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "section name",
                        "inner diameter",
                        "outer diameter",
                        "depth set at"});
            table2.AddRow(new string[] {
                        "Surface",
                        "102",
                        "102",
                        "500"});
#line 10
testRunner.And("the casings are set as follows:", ((string)(null)), table2, "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Scenario 1")]
        public virtual void Scenario1()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scenario 1", ((string[])(null)));
#line 14
this.ScenarioSetup(scenarioInfo);
#line 5
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "description"});
            table3.AddRow(new string[] {
                        "Walmart",
                        "a large retailer"});
            table3.AddRow(new string[] {
                        "Sam\'s Local Deli",
                        "a small restaurant"});
            table3.AddRow(new string[] {
                        "Costco",
                        "a large retailer"});
#line 16
testRunner.Given("the following customers are available:", ((string)(null)), table3, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "username",
                        "password",
                        "access level",
                        "customer"});
            table4.AddRow(new string[] {
                        "jonathan",
                        "password",
                        "report-only",
                        "Walmart"});
            table4.AddRow(new string[] {
                        "fred",
                        "password",
                        "applicant",
                        "Sam\'s Local Deli"});
            table4.AddRow(new string[] {
                        "sam",
                        "password",
                        "admin",
                        "Costco"});
#line 22
testRunner.And("the following users are available:", ((string)(null)), table4, "And ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "section name",
                        "inner diameter",
                        "outer diameter",
                        "length",
                        "type"});
            table5.AddRow(new string[] {
                        "Drill Pipe 1",
                        "70",
                        "130",
                        "3333",
                        "DP"});
            table5.AddRow(new string[] {
                        "Heavy Weight Drill Pipe 1",
                        "70",
                        "180",
                        "300",
                        "HWDP"});
            table5.AddRow(new string[] {
                        "Drill Collars",
                        "72",
                        "185",
                        "25",
                        "DC"});
#line 29
testRunner.Given("the drilling assembly is (top to bottom):", ((string)(null)), table5, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "section name",
                        "inner diameter",
                        "outer diameter",
                        "depth set at"});
            table6.AddRow(new string[] {
                        "Surface",
                        "102",
                        "102",
                        "500"});
#line 35
testRunner.And("the casings are set as follows:", ((string)(null)), table6, "And ");
#line 39
testRunner.When("I hit the calculate Annular Velocity button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "section name",
                        "annular velocity"});
            table7.AddRow(new string[] {
                        "Drill Pipe 1",
                        "144.22"});
#line 40
testRunner.Then("annular velocity for the drill string is (top to bottom):", ((string)(null)), table7, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Scenario 2")]
        public virtual void Scenario2()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scenario 2", ((string[])(null)));
#line 44
this.ScenarioSetup(scenarioInfo);
#line 5
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "description"});
            table8.AddRow(new string[] {
                        "Walmart",
                        "a large retailer"});
            table8.AddRow(new string[] {
                        "Sam\'s Local Deli",
                        "a small restaurant"});
            table8.AddRow(new string[] {
                        "Costco",
                        "a large retailer"});
#line 46
testRunner.Given("the following customers are available:", ((string)(null)), table8, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "username",
                        "password",
                        "access level",
                        "customer"});
            table9.AddRow(new string[] {
                        "jonathan",
                        "password",
                        "report-only",
                        "Walmart"});
            table9.AddRow(new string[] {
                        "fred",
                        "password",
                        "applicant",
                        "Sam\'s Local Deli"});
            table9.AddRow(new string[] {
                        "sam",
                        "password",
                        "admin",
                        "Costco"});
#line 52
testRunner.And("the following users are available:", ((string)(null)), table9, "And ");
#line 59
testRunner.Given("a user is logged with the role \'view-only\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 60
testRunner.When("they select reports menu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 61
testRunner.Then("they will only see the the report named \'quarterly sales report\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
