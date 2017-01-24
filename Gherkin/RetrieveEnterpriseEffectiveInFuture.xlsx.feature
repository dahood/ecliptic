
Feature: RetrieveEnterpriseEffectiveInFuture


Scenario: SearchByIdAndGasDay
Given Enterprises
| Id      | Legal Name                 | Short Name |
| 1000034 | Transcanada Pipelines Inc. | TCPL       |

And Account
| Time     | Amount | Balance |
| 1/1/2012 | 15     | 15      |
| 1/2/2012 | 15     | 30      |
| 1/3/2012 | 15     | 45      |
| 1/4/2012 | 15     | 60      |
| 1/5/2012 | 15     | 75      |

When Search Enterprise using
| Id      |
| 1000034 |

Then View Enterprise
| Id      | Legal Name                 | Short Name |
| 1000034 | Transcanada Pipelines Inc. | TCPL       |

