﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wypozyczalniaSamochodow;

namespace wypozyczalniaSamochodow
{
    public partial class CarConditionPanel : UserControl
    {
        Reservation reservation;
        //pole 'showParent' zostało dodane ze względu na wymagania frameworka Windows.Forms
        //pole przechowuje metodę panelu nadrzędnego
        public Action showParent;
        public CarConditionPanel()
        {
            InitializeComponent();
            finePanel.Hide();
        }

        public void Show(Reservation reservation)
        {
            this.reservation = reservation;
            Show();
            BringToFront();
        }

        private async void save(object sender, EventArgs e)
        {
            if (fineCheckbox.Checked)
            {
                Fine fine = new Fine();
                await saveFine(fine,reservation).ContinueWith( async task =>
                {
                    int fineId = (int)task.Result;
                    if(fineId > 0)
                    {

                    reservation.fineId = fineId;
                    reservation._checked = true;
                    await DatabaseService.updateReservation(reservation);
                    }
                    else
                    {
                        MessageBox.Show("Przepraszamy, nie udało się zapisać opłaty.");
                    }
                });
            }
            if (!efficientCheckbox.Checked)
            {
                Car car = new Car();
                car.registrationNumber = reservation.registrationNumber;
                car.carEfficiency = efficientCheckbox.Checked;
                await DatabaseService.updateCar(car);
            }
            showParent();
        }

        private async Task<long> saveFine(Fine fine,Reservation reservation)
        {
            fine.fineCost = (double)cost.Value;
            fine.fineDescription = description.Text;
            return await DatabaseService.insertFine(fine,reservation);

        }
        //metoda 'fineCheckbox_CheckedChanged' została dodana ze względu na wymagania frameworka Windows.Forms
        //jest uruchamiana gdy użytkownik zmieni wartość pola wyboru
        private void fineCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (fineCheckbox.Checked)
            {
                finePanel.Show();
            }
            else
            {
                finePanel.Hide();
            }
        }
    }
}
