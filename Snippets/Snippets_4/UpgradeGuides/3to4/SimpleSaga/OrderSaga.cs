﻿namespace Snippets4.UpgradeGuides._3to4.SimpleSaga
{
    using NServiceBus;
    using NServiceBus.Saga;

    #region 3to4ConfigureHowToFindSaga

    public class OrderSaga : Saga<OrderSagaData>,
                            IAmStartedByMessages<StartOrder>,
                            IHandleMessages<CompleteOrder>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<CompleteOrder>(m => m.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
        }

    #endregion

        public void Handle(StartOrder message)
        {
            Data.OrderId = message.OrderId;
        }

        public void Handle(CompleteOrder message)
        {
            // code to handle order completion
            MarkAsComplete();
        }
    }


}
