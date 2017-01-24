using TechTalk.SpecFlow;

namespace Ecliptic.Unit.Test.Stories.Setup
{
    [Binding]
    public class CustomerSetupSteps
    {
        [Binding]
        public class StepDefinitions
        {
            [Given(@"the following customers are available:")]
            public void GivenTheFollowingCustomersAreAvailable(Table table)
            {
            }

            [Given(@"the following users are available:")]
            public void GivenTheFollowingUsersAreAvailable(Table table)
            {
            }
        }
    }
}
