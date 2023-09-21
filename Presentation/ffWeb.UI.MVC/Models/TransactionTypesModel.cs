using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace ffWeb.UI.MVC.Models
{
    public class TransactionTypesModel
    { 
        public int TransactionTypeID { get; set; }
         
        public string DebitCredit { get; set; }
         
        public string ShortCode { get; set; }
         
        public short TxnClass { get; set; }
         
        public string Description { get; set; }
         
        public decimal DefaultAmount { get; set; }
         
        public string AmountExpression { get; set; }
         
        public int DefaultMainAccount { get; set; }
         
        public int DefaultContraAccount { get; set; }
         
        public short NarrativeFlag { get; set; }
         
        public string DefaultMainNarrative { get; set; }
         
        public string DefaultContraNarrative { get; set; }
         
        public short TxnTypeView { get; set; }
         
        public bool ChargeCommission { get; set; }
         
        public bool ChargeCommissionToTransaction { get; set; }
         
        public string CommissionAmountExpression { get; set; }
         
        public bool CommissionDrAnotherAccount { get; set; }
         
        public int CommissionDrAccount { get; set; }
         
        public int CommissionCrAccount { get; set; }
         
        public short CommissionNarrativeFlag { get; set; }
         
        public string CommissionMainNarrative { get; set; }
         
        public string CommissionContraNarrative { get; set; }
         
        public string CommComputationMethod { get; set; }
         
        public bool Absolute { get; set; }
         
        public int TieredTableId { get; set; }
         
        public decimal CommissionAmount { get; set; }
         
        public short DialogFlag { get; set; }
         
        public bool ForcePost { get; set; }
         
        public string Screen { get; set; }
         
        public short ValueDateOffset { get; set; }
         
        public string ChargeWho { get; set; }
         
        public string DrCommCalcMethod { get; set; }
         
        public string CrCommCalcMethod { get; set; }


    }
}