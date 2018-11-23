using System;
using KanKanCore;
using KanKanCore.Karass;
using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest
{
    public class KanKanMessageTests
    {
        string _firstFrameMessage = string.Empty;
        string _secondFrameMessage = string.Empty;

        [Fact]
        public void UActionRunnerHasSendMessageMethod()
        {
            IKarassMessage karassMessage = new KarassMessage();
            Karass<string> karass = new KarassStringDummy();
            KanKan kanKan = new KanKan(karass,karassMessage);
            kanKan.SendMessage("Cat");

        }
        
        [Fact]
        public void UActionRunnerSendMessageSendsMessageToNextFrame()
        {
            IKarassMessage karassMessage = new KarassMessage();
            KarassFramesStub karass = new KarassFramesStub(new Func<string,bool>[]{FirstFrameSpy,SecondFrameSpy});
            KanKan kanKan = new KanKan(karass,karassMessage);
            
            kanKan.SendMessage("Cat");
            kanKan.MoveNext();
       
            Assert.True(_firstFrameMessage == "Cat");
         
            kanKan.SendMessage("Doggo");
            kanKan.MoveNext();
            Assert.True(_secondFrameMessage == "Doggo");
           
            
        }
        
        [Fact]
        public void MessagesOnlyLastForOneFrame()
        {
            IKarassMessage karassMessage = new KarassMessage();
            KarassFramesStub karass = new KarassFramesStub(new Func<string,bool>[]{FirstFrameSpy,SecondFrameSpy});
            KanKan kanKan = new KanKan(karass,karassMessage);
            
            kanKan.SendMessage("Cat");
            kanKan.MoveNext();
            Assert.True(_firstFrameMessage == "Cat");
            
            kanKan.MoveNext();
            Assert.True(_secondFrameMessage == string.Empty);
        }

        private bool FirstFrameSpy(string s)
        {
            _firstFrameMessage = s;
            return true;
        }

        private bool SecondFrameSpy(string s)
        {
            _secondFrameMessage = s;
            return false;
        }
    }
}