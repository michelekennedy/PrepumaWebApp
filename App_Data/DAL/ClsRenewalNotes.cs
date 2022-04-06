using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsRenewalNotes
    {

        public int idContractRenewalNotes { get; set; }
        public int idContractRenewal { get; set; }
        public string Note { get; set; }
        public string NoteType { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? ActiveFlag { get; set; }

        public static List<ClsRenewalNotes> GetContractRenewalNotes(int idContractRenewal)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ClsRenewalNotes> oNoteList = (from data in prepumaContext.GetTable<tblContractRenewalNote>()
                                                  where data.idContractRenewal == idContractRenewal
                                                  where data.ActiveFlag != false
                                                  orderby data.CreatedOn

                                                select new ClsRenewalNotes
                                                  {
                                                      idContractRenewalNotes = data.idContractRenewalNotes,
                                                      idContractRenewal = data.idContractRenewal,
                                                      Note = data.Note,
                                                      NoteType = data.NoteType,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      ActiveFlag = (bool)data.ActiveFlag

                                                  }).ToList<ClsRenewalNotes>();

            return oNoteList;
        }

        public static List<ClsRenewalNotes> GetContractRenewalApprovalNotes(int idContractRenewal)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ClsRenewalNotes> oNoteList = (from data in prepumaContext.GetTable<tblContractRenewalNote>()
                                               where data.idContractRenewal == idContractRenewal
                                               where data.ActiveFlag != false
                                               where data.NoteType == "approval"
                                               orderby data.CreatedOn

                                               select new ClsRenewalNotes
                                               {
                                                   idContractRenewalNotes = data.idContractRenewalNotes,
                                                   idContractRenewal = data.idContractRenewal,
                                                   Note = data.Note,
                                                   NoteType = data.NoteType,
                                                   UpdatedBy = data.UpdatedBy,
                                                   UpdatedOn = data.UpdatedOn,
                                                   CreatedBy = data.CreatedBy,
                                                   CreatedOn = data.CreatedOn,
                                                   ActiveFlag = (bool)data.ActiveFlag

                                               }).ToList<ClsRenewalNotes>();

            return oNoteList;
        }

        public static ClsRenewalNotes GetNote(int idContractRenewalNote)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsRenewalNotes oNote = (from data in prepumaContext.GetTable<tblContractRenewalNote>()
                                               where data.idContractRenewalNotes == idContractRenewalNote
                                               where data.ActiveFlag != false
                                               select new ClsRenewalNotes
                                               {
                                                   idContractRenewalNotes = data.idContractRenewalNotes,
                                                   idContractRenewal = data.idContractRenewal,
                                                   Note = data.Note,
                                                   NoteType = data.NoteType,
                                                   UpdatedBy = data.UpdatedBy,
                                                   UpdatedOn = data.UpdatedOn,
                                                   CreatedBy = data.CreatedBy,
                                                   CreatedOn = data.CreatedOn,
                                                   ActiveFlag = (bool)data.ActiveFlag

                                               }).SingleOrDefault();

            return oNote;
        }

        public static void SaveNote(ClsRenewalNotes note)
        {

            
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

             tblContractRenewalNote oNewRow = new tblContractRenewalNote()
                {
                    
                    idContractRenewal = note.idContractRenewal,
                    Note = note.Note,
                    NoteType = note.NoteType,
                    CreatedBy = note.CreatedBy,
                    CreatedOn = note.CreatedOn,
                    UpdatedBy = note.UpdatedBy,
                    UpdatedOn = note.UpdatedOn,
                    ActiveFlag = note.ActiveFlag
                };

                // Add the new object to the contracts collection.
                prepumaContext.GetTable<tblContractRenewalNote>().InsertOnSubmit(oNewRow);
                prepumaContext.SubmitChanges();

                oNewRow.idContractRenewalNotes = oNewRow.idContractRenewalNotes;

        }

        public static void DeactivateNote(int idNote, string userid)
        {

            ClsRenewalNotes oExisting = GetNote(idNote);


            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            if (oExisting != null)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in prepumaContext.GetTable<tblContractRenewalNote>()
                    where qdata.idContractRenewalNotes == oExisting.idContractRenewalNotes
                    select qdata;


                foreach (tblContractRenewalNote updRow in query)
                {
                    updRow.ActiveFlag = false;
                    updRow.UpdatedBy = userid;
                    updRow.UpdatedOn = DateTime.Now;
                }

                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();
            }

        }


    }
}