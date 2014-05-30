using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomText;

namespace ProducersConsumersWithPasswords
{
    public class PasswordConsumer
    {       
        private FormattedText desiredPassword;

        private volatile bool cancelationFlag = false;
        private object synchronizationObject = new object();
        private Int32 passwordTakingMaxiumTime = 200;
        private Task worker;

        public event Action OnDesiredPasswordFound;

        public bool IsConsumtionInProgress
        {
            get;
            private set;
        }

        public FormattedText DesiredPassword
        {
            get { return desiredPassword; }
        }

        public Int32 PasswordTakingMaximumTime
        {
            get { return passwordTakingMaxiumTime; }
            set { passwordTakingMaxiumTime = value; }
        }

        public PasswordConsumer(FormattedText desiredPassword)
        {
            if (desiredPassword == null)
                throw new NullReferenceException();
            this.desiredPassword = desiredPassword;
            IsConsumtionInProgress = false;
        }

        public void StartConsumption(SmartWarehouse<FormattedText> warehouse)
        {
            if (warehouse == null)
                throw new NullReferenceException();
            lock (synchronizationObject)
            {
                if (IsConsumtionInProgress)
                    throw new InvalidOperationException("Consumption is already in progress.");

                IsConsumtionInProgress = true;
                worker = Task.Run(() =>
                    {
                        Consumption(warehouse);
                    });               
            }
        }

        public void StopConsumption()
        {
            cancelationFlag = true;
            lock (synchronizationObject)
            {
                worker.Wait();
            }
        }

        private void Consumption(SmartWarehouse<FormattedText> warehouse)
        {
            cancelationFlag = false;
            bool passwordFound = false;

            while (!cancelationFlag && !passwordFound)
            {
                Console.WriteLine("Consumer is trying to take the Password.");
                FormattedText temporaryPassword;
                bool passwordWasTaken = warehouse.TryTake(out temporaryPassword, PasswordTakingMaximumTime);

                if (passwordWasTaken)
                {
                    Console.WriteLine("Consumer took the Password.");
                    if (temporaryPassword.CompareTo(desiredPassword) == 0)
                    {
                        Console.WriteLine("Consumer found the Password.");
                        passwordFound = true;

                        if (OnDesiredPasswordFound != null)
                            OnDesiredPasswordFound();
                    }
                }
                else
                    Console.WriteLine("Consumer was not able to take the Password due to maximum waiting time.");
            }

            IsConsumtionInProgress = false;
        }
    }
}
