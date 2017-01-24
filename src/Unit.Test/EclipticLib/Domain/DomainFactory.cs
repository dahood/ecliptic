using System;
using System.Collections.Generic;
using EclipticLib.Generation.Domain;
using EclipticLib.Generation.Domain.Gherkin;

namespace Ecliptic.Unit.Test.EclipticLib.Domain
{
    public static class DomainFactory
    {
        public static Feature BasicFeature()
        {
            return new Feature
            {
                Title = "FeatureTest",
                Description = "Unit Test for Feature ToString method",
                Tags = new List<string> { "storey123", "tag2" },
                Scenarios = new List<Scenario> { CreateScenario() }
            };
        }
        
        public static Feature FeatureWithBackground()
        {
            return new Feature
            {
                Title = "Feature Background Test",
                Description = "Feature with background",
                Tags = new List<string> { "storeyxyZ"},
                Background = CreateBackgroundScenario(),
                Scenarios = new List<Scenario> { CreateScenario() }
            };
        }

        public static Feature FeatureWithSetup()
        {
            var scenario = CreateScenario();
            scenario.Setup = CreateSetupFeature();

            return new Feature
            {
                Title = "Feature Setup Test",
                Description = "Feature with Setup",
                Tags = new List<string> { "storeyxyZ" },
                Scenarios = new List<Scenario> { scenario }
            };
        }

        private static SetupFeature CreateSetupFeature()
        {
            var underlyingFeature = new Feature
            {
                Title = "Setup",
                Description = "Setup",
                Tags = new List<string> {"setup"},
                Scenarios = new List<Scenario> { CreateSetupScenario() }
            };

            return new SetupFeature(underlyingFeature);
        }

        public static Scenario CreateBackgroundScenario()
        {
            var table = new GherkinTable
            {
                Columns = new List<GherkinColumn> {new GherkinColumn("EnterpriseId"), new GherkinColumn("Name")}
            };
            var row1 = new GherkinRow(table);
            row1.AddContent("123003", 0);
            row1.AddContent("Acme", 1);
            
            var row2 = new GherkinRow(table);
            row2.AddContent("1001", 0);
            row2.AddContent("Cyote Inc.", 1);

            table.Rows = new List<GherkinRow> { row1, row2 };
            return new Scenario
            {
                Title = "Create Enterprises",
                IsBackground = true,
                IsSetup = false,
                Statements = new List<Statement>
                {
                    new Statement
                    {
                        Command = GherkinKeyword.Given,
                        Description = "I create the following enterprises:",
                        Table = table
                        }
                    }
                };
        }

        public static Scenario CreateScenario(Action<Scenario> initializer = null)
        {
            var scenario = new Scenario
            {
                Title = "Scenario 1",
                IsBackground = false,
                IsSetup = false,
                Statements = new List<Statement> {BasicStatement()}
            };

            if (initializer != null)
                initializer(scenario);
            return scenario;
        }

        private static Statement BasicStatement()
        {
            var table = new GherkinTable
            {
                Columns = new List<GherkinColumn> {new GherkinColumn("col1"), new GherkinColumn("col2")}
            };
            var row1 = new GherkinRow(table);
            row1.AddContent("row1-col1", 0);
            row1.AddContent("row1-col2", 1);
            table.Rows = new List<GherkinRow> { row1  };
            return new Statement
            {
                Command = GherkinKeyword.Given,
                Description = "I have the following:",
                Table = table
            };
        }

        public static Scenario CreateSetupScenario()
        {
            var table = new GherkinTable
            {
                Columns = new List<GherkinColumn> {new GherkinColumn("Name"), new GherkinColumn("Age")}
            };
            var row1 = new GherkinRow(table);
            row1.AddContent("Elvis", 0);
            row1.AddContent("64", 1);

            table.Rows = new List<GherkinRow> { row1 };
            return new Scenario
            {
                IsBackground = false,
                IsSetup = true,
                Title = "Setup Scenario",
                Statements = new List<Statement>
                {
                    new Statement
                    {
                        Command = GherkinKeyword.Given,
                        Description = "I am using this Setup",
                        Table = table
                    }
                }
            };
        }
    }

    public class FeatureSpecflowTranslation
    {
        public const string BasicFeature =
@"@storey123 @tag2 
Feature: FeatureTest
Unit Test for Feature ToString method

Scenario: Scenario 1
Given I have the following:
| col1      | col2      |
| row1-col1 | row1-col2 |

";

        public const string FeatureWithBackground =
@"@storeyxyZ 
Feature: Feature Background Test
Feature with background

Background:
Given I create the following enterprises:
| EnterpriseId | Name       |
| 123003       | Acme       |
| 1001         | Cyote Inc. |

Scenario: Scenario 1
Given I have the following:
| col1      | col2      |
| row1-col1 | row1-col2 |

";

        public static string FeatureWithSetup =
@"@storeyxyZ 
Feature: Feature Setup Test
Feature with Setup

Scenario: Scenario 1
Setup Scenario
Given I am using this Setup
| Name  | Age |
| Elvis | 64  |


Given I have the following:
| col1      | col2      |
| row1-col1 | row1-col2 |

";

        public static string BasicScenario =
@"Scenario: Scenario 1
Given I have the following:
| col1      | col2      |
| row1-col1 | row1-col2 |

";

        public static string SetupScenario =
@"Setup Scenario
Given I am using this Setup
| Name  | Age |
| Elvis | 64  |

";

        public static string BackgroundScenario =
@"Background:
Given I create the following enterprises:
| EnterpriseId | Name       |
| 123003       | Acme       |
| 1001         | Cyote Inc. |

";
    }
}
