using gssManRegistration.Models;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Z.EntityFramework.Extensions;

namespace gssManRegistration.Controllers
{
    public class StudentTransferController : ApiController
    {
        private readonly gssManRegistrationDbContext studentContext = new gssManRegistrationDbContext();
        private readonly sarbetMarkDbContext sarbetMarkContext = new sarbetMarkDbContext();
        private readonly kolfeMarkDbContext kolfeMarkContext = new kolfeMarkDbContext();
        private readonly laftoMarkDbContext laftoMarkContext = new laftoMarkDbContext();
        private readonly boleMarkDbContext boleMarkContext = new boleMarkDbContext();
        private readonly mekMarkDbContext mekMarkContext = new mekMarkDbContext();
        private readonly megMarkDbContext megMarkContext = new megMarkDbContext();
        private readonly cmcMarkDbContext cmcMarkContext = new cmcMarkDbContext();
    
        // GET api/studentransfer?studentId={studentId}&sourceBranch={sourceBranch}&destinationBranch={destinationBranch}
        public IHttpActionResult Transfer(string studentId, string sourceBranch, string destinationBranch)
        {
            var std = studentContext.tbl_student.FirstOrDefault(s => s.studentid == studentId);
             var source = sourceBranch;
            var currentAcademicYear = (DateTime.Now.Year - 1) + $"{DateTime.Now.Year}";
            var studentid = std.studentid;
            var destination = destinationBranch;           
            var success = "Operation Successful";
            var message = "Confirm that the student with the id:" + $"{studentid} exists and that your destination and source branch are different";

            /// case: 1 -- LAFTO            
            var copyLaftoTestMarks = laftoMarkContext.testmarks20212022.ToList().Where(s => s.studentid == studentid && s.academicyear.Equals(currentAcademicYear));
            var copyLaftoWeeklyMarks = laftoMarkContext.weeklymarks20212022.ToList().Where(s => s.studentid == studentid && s.acyear.Equals(currentAcademicYear));

            /// case: 2 -- KOLFE                  
            var copyKolfeTestMarks = kolfeMarkContext.testmarks20212022.ToList().Where(s => s.studentid == studentid && s.academicyear.Equals(currentAcademicYear));
            var copyKolfeWeeklyMarks = kolfeMarkContext.weeklymarks20212022.ToList().Where(s => s.studentid == studentid && s.acyear.Equals(currentAcademicYear));

            /// case: 3 -- SARBET
            var copySarbetTestMarks = sarbetMarkContext.testmarks20212022.ToList().Where(s => s.studentid == studentid && s.academicyear.Equals(currentAcademicYear));
            var copySarbetWeeklyMarks = sarbetMarkContext.weeklymarks20212022.ToList().Where(s => s.studentid == studentid && s.acyear.Equals(currentAcademicYear));

            /// case: 4 -- MEK           
            var copyMekTestMarks = mekMarkContext.testmarks20212022.ToList().Where(s => s.studentid == studentid && s.academicyear.Equals(currentAcademicYear));
            var copyMekWeeklyMarks = mekMarkContext.weeklymarks20212022.ToList().Where(s => s.studentid == studentid && s.acyear.Equals(currentAcademicYear));

            /// case: 5 -- MEG           
            var copyMegTestMarks = megMarkContext.testmarks20212022.ToList().Where(s => s.studentid == studentid && s.academicyear.Equals(currentAcademicYear));
            var copyMegWeeklyMarks = megMarkContext.weeklymarks20212022.ToList().Where(s => s.studentid == studentid && s.acyear.Equals(currentAcademicYear));

            /// case: 6 -- CMC            
            var copyCmcTestMarks = cmcMarkContext.testmarks20212022.ToList().Where(s => s.studentid == studentid && s.academicyear.Equals(currentAcademicYear));
            var copyCmcWeeklyMarks = cmcMarkContext.weeklymarks20212022.ToList().Where(s => s.studentid == studentid && s.acyear.Equals(currentAcademicYear));

            /// case: 7 -- BOLE            
            var copyBoleTestMarks = boleMarkContext.testmarks20212022.ToList().Where(s => s.studentid == studentid && s.academicyear.Equals(currentAcademicYear));
            var copyBoleWeeklyMarks = boleMarkContext.weeklymarks20212022.ToList().Where(s => s.studentid == studentid && s.acyear.Equals(currentAcademicYear));

            if (studentid == null || source == destination)
            {
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            /// CASE 1:KOLFE TO OTHER BRANCHES
            if (sourceBranch == "KOLFE" && destinationBranch == "LAFTO")
            {
                laftoMarkContext.BulkInsert(copyKolfeTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                laftoMarkContext.BulkInsert(copyKolfeWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "KOLFE" && destination == "SARBET")
            {
                sarbetMarkContext.BulkInsert(copyKolfeTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                sarbetMarkContext.BulkInsert(copyKolfeWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (source == "KOLFE" && destination == "MEK")
            {
                mekMarkContext.BulkInsert(copyKolfeTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });               
                mekMarkContext.BulkInsert(copyKolfeWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });               
                
                return Ok(success);
            }
            if (source == "KOLFE" && destination == "MEG")
            {
                megMarkContext.BulkInsert(copyKolfeTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                megMarkContext.BulkInsert(copyKolfeWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
               
                return Ok(success);
            }
            if (source == "KOLFE" && destination == "CMC")
            {
                cmcMarkContext.BulkInsert(copyKolfeTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                cmcMarkContext.BulkInsert(copyKolfeWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (sourceBranch == "KOLFE" && destinationBranch == "BOLE")
            {
                boleMarkContext.BulkInsert(copyKolfeTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                boleMarkContext.BulkInsert(copyKolfeWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
               
                return Ok(success);
            }

            // CASE 2:LAFTO TO OTHER BRANCHES
            if (source == "LAFTO" && destination == "KOLFE")
            {
                kolfeMarkContext.BulkInsert(copyLaftoTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                kolfeMarkContext.BulkInsert(copyLaftoWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (source == "LAFTO" && destination == "SARBET")
            {
                sarbetMarkContext.BulkInsert(copyLaftoTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                sarbetMarkContext.BulkInsert(copyLaftoWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (source == "LAFTO" && destination == "MEK")
            {
                mekMarkContext.BulkInsert(copyLaftoTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                mekMarkContext.BulkInsert(copyLaftoWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (source == "LAFTO" && destination == "MEG")
            {
                megMarkContext.BulkInsert(copyLaftoTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                megMarkContext.BulkInsert(copyLaftoWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
               
                return Ok(success);
            }
            if (source == "LAFTO" && destination == "CMC")
            {
                cmcMarkContext.BulkInsert(copyLaftoTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                cmcMarkContext.BulkInsert(copyLaftoWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (source == "LAFTO" && destination == "BOLE")
            {
                boleMarkContext.BulkInsert(copyLaftoTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                boleMarkContext.BulkInsert(copyLaftoWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
               
                return Ok(success);
            }

            // CASE 3:SARBET TO OTHER BRANCHES
            if (source == "SARBET" && destination == "KOLFE")
            {
                kolfeMarkContext.BulkInsert(copySarbetTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                kolfeMarkContext.BulkInsert(copySarbetWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (source == "SARBET" && destination == "SARBET")
            {
                sarbetMarkContext.BulkInsert(copySarbetTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                sarbetMarkContext.BulkInsert(copySarbetWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (source == "SARBET" && destination == "MEK")
            {
                mekMarkContext.BulkInsert(copySarbetTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                mekMarkContext.BulkInsert(copySarbetWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (sourceBranch == "SARBET" && destinationBranch == "MEG")
            {
                megMarkContext.BulkInsert(copySarbetTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                megMarkContext.BulkInsert(copySarbetWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
               
                return Ok(success);
            }
            if (source == "SARBET" && destination == "CMC")
            {
                cmcMarkContext.BulkInsert(copySarbetTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                cmcMarkContext.BulkInsert(copySarbetWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (source == "SARBET" && destination == "BOLE")
            {
                boleMarkContext.BulkInsert(copySarbetTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                boleMarkContext.BulkInsert(copySarbetWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
               
                return Ok(success);
            }

            // CASE 4:MEK TO OTHER BRANCHES
            if (source == "MEK" && destination == "KOLFE")
            {
                kolfeMarkContext.BulkInsert(copyMekTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                kolfeMarkContext.BulkInsert(copyMekWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "MEK" && destination == "SARBET")
            {
                sarbetMarkContext.BulkInsert(copyMekTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                sarbetMarkContext.BulkInsert(copyMekWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "MEK" && destination == "MEK")
            {
                mekMarkContext.BulkInsert(copyMekTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                mekMarkContext.BulkInsert(copyMekWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                
                return Ok(success);
            }
            if (source == "MEK" && destination == "MEG")
            {
                megMarkContext.BulkInsert(copyMekTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                megMarkContext.BulkInsert(copyMekWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
               
                return Ok(success);
            }
            if (source == "MEK" && destination == "CMC")
            {
                cmcMarkContext.BulkInsert(copyMekTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                cmcMarkContext.BulkInsert(copyMekWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "MEK" && destination == "BOLE")
            {
                boleMarkContext.BulkInsert(copyMekTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                boleMarkContext.BulkInsert(copyMekWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
               
                return Ok(success);
            }

            // CASE 5:MEG TO OTHER BRANCHES
            if (source == "MEG" && destination == "KOLFE")
            {
                kolfeMarkContext.BulkInsert(copyMegTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                kolfeMarkContext.BulkInsert(copyMegWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "MEG" && destination == "SARBET")
            {
                sarbetMarkContext.BulkInsert(copyMegTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                sarbetMarkContext.BulkInsert(copyMegWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (sourceBranch == "MEG" && destination == "MEK")
            {
                mekMarkContext.BulkInsert(copyMegTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                mekMarkContext.BulkInsert(copyMegWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "MEG" && destination == "MEG")
            {
                megMarkContext.BulkInsert(copyMegTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                megMarkContext.BulkInsert(copyMegWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
               
                return Ok(success);
            }
            if (source == "MEG" && destination == "CMC")
            {
                cmcMarkContext.BulkInsert(copyMegTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                cmcMarkContext.BulkInsert(copyMegWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "MEG" && destination == "BOLE")
            {
                boleMarkContext.BulkInsert(copyMegTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                boleMarkContext.BulkInsert(copyMegWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
               
                return Ok(success);
            }

            // CASE 6:CMC TO OTHER BRANCHES
            if (source == "CMC" && destination == "KOLFE")
            {
                kolfeMarkContext.BulkInsert(copyCmcTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                kolfeMarkContext.BulkInsert(copyCmcWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "CMC" && destination == "SARBET")
            {
                sarbetMarkContext.BulkInsert(copyCmcTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                sarbetMarkContext.BulkInsert(copyCmcWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "CMC" && destinationBranch == "MEK")
            {
                mekMarkContext.BulkInsert(copyCmcTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                mekMarkContext.BulkInsert(copyCmcWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "CMC" && destination == "MEG")
            {
                megMarkContext.BulkInsert(copyCmcTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                megMarkContext.BulkInsert(copyCmcWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
               
                return Ok(success);
            }
            if (source == "CMC" && destination == "CMC")
            {
                cmcMarkContext.BulkInsert(copyCmcTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                cmcMarkContext.BulkInsert(copyCmcWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "CMC" && destination == "BOLE")
            {
                boleMarkContext.BulkInsert(copyCmcTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                boleMarkContext.BulkInsert(copyCmcWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
               
                return Ok(success);
            }

            // CASE 7:BOLE TO OTHER BRANCHES
            if (source == "BOLE" && destination == "KOLFE")
            {
                kolfeMarkContext.BulkInsert(copyBoleTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                kolfeMarkContext.BulkInsert(copyBoleWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "BOLE" && destination == "LAFTO")
            {
                laftoMarkContext.BulkInsert(copyBoleTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                laftoMarkContext.BulkInsert(copyBoleWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "BOLE" && destination == "SARBET")
            {
                sarbetMarkContext.BulkInsert(copyBoleTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                sarbetMarkContext.BulkInsert(copyBoleWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "BOLE" && destination == "MEK")
            {
                mekMarkContext.BulkInsert(copyBoleTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                mekMarkContext.BulkInsert(copyBoleWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }
            if (source == "BOLE" && destination == "MEG")
            {
                megMarkContext.BulkInsert(copyBoleTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                megMarkContext.BulkInsert(copyBoleWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
               
                return Ok(success);
            }
            if (source == "BOLE" && destination == "CMC")
            {
                cmcMarkContext.BulkInsert(copyBoleTestMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.testtype,
                        mark.academicyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });
                cmcMarkContext.BulkInsert(copyBoleWeeklyMarks, options => {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = mark => new
                    {
                        mark.subjectid,
                        mark.acyear,
                        mark.quarternumber,
                        mark.weeknumber,
                        mark.description,
                        mark.studentid,
                        mark.markid,
                    };
                });                
                
                return Ok(success);
            }

            return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
        } 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                studentContext.Dispose();
                kolfeMarkContext.Dispose();
                laftoMarkContext.Dispose();
                sarbetMarkContext.Dispose();
                mekMarkContext.Dispose();
                megMarkContext.Dispose();
                cmcMarkContext.Dispose();
                boleMarkContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}