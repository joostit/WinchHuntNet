using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace JoostIT.WinchHunt.WinchHuntNet.IntegrationTests.Steps
{
    [Binding]
    public class WinchuntConnectorConnectionsSteps
    {

        private readonly ScenarioContext _scenarioContext;

        private WinchHuntConnector connector;

        public WinchuntConnectorConnectionsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"A new and default WinchuntConnector")]
        public void GivenANewAndDefaultWinchuntConnector()
        {
            connector = new WinchHuntConnector();
        }
        
        [When(@"I do nothing")]
        public void WhenIDoNothing()
        {
            // Do nothing
        }
        
        [Then(@"It should not be connected")]
        public void ThenItShouldNotBeConnected()
        {
            Assert.IsFalse(connector.IsConnected);
        }
    }
}
