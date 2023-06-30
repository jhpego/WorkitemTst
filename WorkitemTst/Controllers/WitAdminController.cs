using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using FieldType = Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models.FieldType;
using System.Diagnostics;
using System.Security.Policy;
using System.Text.Json.Nodes;
using System.Text;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using WorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using System.Xml.Serialization;
using WorkitemTst.Models;
using System.Reflection.PortableExecutable;
using Microsoft.VisualStudio.Services.Users;
using WorkItemType = WorkitemTst.Models.WorkItemType;
using WorkitemTst.Entitys;
using System.Runtime.Serialization;
using System.Xml;
using WorkitemTst.Facade;
using Microsoft.TeamFoundation.Build.WebApi;

namespace WorkitemTst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WitAdminController : ControllerBase
    {
        private readonly Tfs _tfs;
        private readonly AppOptions _appOptions;

        public WitAdminController(
            Tfs tfs ,
            AppOptions appOptions
            )
        {
            _tfs = tfs;
            _appOptions = appOptions;
            }



        /// <summary>
        /// Gets a list of WorkItem Types of a project (listwitd)
        /// </summary>
        [HttpGet("workitemType")]
        public List<string> GetWorkitemTypeList()
        {
            return _tfs.GetWorkitemTypeList();
        }


        /// <summary>
        /// Gets the detail of a WorkItem Type (exportwitd)
        /// </summary>
        [HttpGet("workitemType/{wit}")]
        public XmlWorkitemType GetWorkitemType(string wit) {
            return _tfs.GetWorkitemType(wit);
        }


        /// <summary>
        /// Creates or Updates a WorkItem Type in a project scope (importwitd)
        /// </summary>
        [HttpPost("workitemType/{wit}")]
        public string CreateOrUpdateWorkitemType(string wit, [FromBody] WorkitemTypeInput workitem)
        {
            return this._tfs.CreateOrUpdateWorkitemType(wit, workitem);
        }


        /// <summary>
        /// Uploads a Workitem type XML file (importwitd) 
        /// </summary>
        [HttpPost("workitemType/upload")]
        public async Task<string> UploadWit([FromForm] IFormFile file)
        {
            return this._tfs.UploadWorkitemType(file);
        }

        /// <summary>
        /// Renames a Workitem type (renamewitd) 
        /// </summary>
        [HttpPatch("workitemType/{wit}")]
        public async Task<string> RenameWit(string wit, [FromBody] WorkitemTypeInput workitem)
        {
            return this._tfs.RenameWorkitemType(wit, workitem.Name);
        }



        /// <summary>
        /// Deletes a WorkItem Type (destroywitd)
        /// </summary>
        [HttpDelete("workitemType/{wit}")]
        public string DeleteWorkitemType(string wit)
        {
            return  this._tfs.DeleteWorkitemType(wit);
        }



        /// <summary>
        /// Lists all Link Types (listlinktypes)
        /// </summary>
        [HttpGet("linktype")]
        public async Task<IEnumerable<dynamic>> GetLinkTypes()
        { 
            return _tfs.GetLinkTypes();
        }


        /// <summary>
        /// witadmin
        /// </summary>
        [HttpGet("globallist/list")]
        public async Task<IEnumerable<dynamic>> GetGlobalLists()
        {
            return _tfs.GetGlobalLists();
        }


        /// <summary>
        /// witadmin
        /// </summary>
        [HttpGet("globallists")]
        public async Task<dynamic> ExportGlobalLists()
        {
            return _tfs.ExportGlobalLists();
        }


        /// <summary>
        /// witadmin
        /// </summary>
        [HttpDelete("globallists")]
        public async Task<dynamic> DestroyGlobalLists([FromQuery] string globallistName)
        {
            return _tfs.DestroyGlobalList(globallistName);
        }


        /// <summary>
        /// witadmin
        /// </summary>
        [HttpGet("globalworkflow")]
        public async Task<dynamic> ExportGlobalWorkflow([FromQuery] string? project)
        {
            return _tfs.ExportGlobalWorkflow(project);
        }


        /// <summary>
        /// witadmin
        /// </summary>
        [HttpPost("globalworkflow")]
        public async Task<dynamic> ImportGlobalWorkflow([FromQuery] string? project)
        {
            return _tfs.ImportGlobalWorkflow(project);
        }




    }
}
