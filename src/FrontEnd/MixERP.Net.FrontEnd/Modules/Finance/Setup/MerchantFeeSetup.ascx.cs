﻿/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.


MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;
using MixERP.Net.i18n.Resources;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Finance.Setup
{
    public partial class MerchantFeeSetup : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.KeyColumn = "merchant_fee_setup_id";
                scrud.TableSchema = "core";
                scrud.Table = "merchant_fee_setup";
                scrud.ViewSchema = "core";
                scrud.View = "merchant_fee_setup_scrud_view";
                scrud.Text = Titles.MerchantFeeSetup;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.UseDisplayViewsAsParents = true;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.bank_accounts.account_id", DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "BankAccountDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.payment_cards.payment_card_id", DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "PaymentCardDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "AccountDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.bank_accounts.account_id", "core.merchant_account_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.payment_cards.payment_card_id", "core.payment_card_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.merchant_fee_setup_account_selector_view");
            return string.Join(",", displayViews);
        }
    }
}