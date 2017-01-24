
Feature: CustomerSetup


Scenario: DataSetup
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

