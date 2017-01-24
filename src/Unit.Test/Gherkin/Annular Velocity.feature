@Story_123 @ECD 
Feature: Annular Velocity
As a mud engineer I want to know the annular velocity of the drilling string so that I can look at ways to drill ahead faster or more efficiently.

Background:
Given that viscometer readings are as follows:
| VG600 | VG300 | VG200 | VG100 | VG6 | VG3 |
| 120   | 75    | 45    | 22    | 5   | 3   |

And the casings are set as follows:
| section name | inner diameter | outer diameter | depth set at | 
| Surface | 102 | 102 | 500 | 

Scenario: Scenario 1
DataSetup
Given the following customers are available:
| name             | description        |
| Walmart          | a large retailer   |
| Sam's Local Deli | a small restaurant |
| Costco           | a large retailer   |

And the following users are available:
| username | password | access level | customer         |
| jonathan | password | report-only  | Walmart          |
| fred     | password | applicant    | Sam's Local Deli |
| sam      | password | admin        | Costco           |


Given the drilling assembly is (top to bottom):
| section name | inner diameter | outer diameter | length | type | 
| Drill Pipe 1 | 70 | 130 | 3333 | DP | 
| Heavy Weight Drill Pipe 1 | 70 | 180 | 300 | HWDP | 
| Drill Collars | 72 | 185 | 25 | DC | 

And the casings are set as follows:
| section name | inner diameter | outer diameter | depth set at | 
| Surface | 102 | 102 | 500 | 

When I hit the calculate Annular Velocity button
Then annular velocity for the drill string is (top to bottom):
| section name | annular velocity | 
| Drill Pipe 1 | 144.22 | 

Scenario: Scenario 2
DataSetup
Given the following customers are available:
| name | description | 
| Walmart | a large retailer | 
| Sam's Local Deli | a small restaurant | 
| Costco | a large retailer | 

And the following users are available:
| username | password | access level | customer | 
| jonathan | password | report-only | Walmart | 
| fred | password | applicant | Sam's Local Deli | 
| sam | password | admin | Costco | 


Given a user is logged with the role 'view-only'
When they select reports menu
Then they will only see the the report named 'quarterly sales report'
