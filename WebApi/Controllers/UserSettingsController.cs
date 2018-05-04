﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database.Entities;
using WebApi.Models.UserSettingsAjaxModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApi.Database;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserSettingsController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        [Route("api/user-settings/{id}")]
        public IActionResult GetUserSettings(string id)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                //if the user logged in is the same as user we want to get/set.
                if (userId == id)
                {
                    using (DBEntities dbe = new DBEntities())
                    {
                        var user = dbe.Users.SingleOrDefault(u => u.Id == id);
                        return Ok(new UserSettingsAjaxModel() { BTCXPUB = user.BTCXPUB, LTCXPUB = user.LTCXPUB, ETHAccount = user.ETHAccount, XMRAddress = user.XMRAddress, XMRPrivateViewKey = user.XMRPrivateViewKey, XMRPublicViewKey = user.XMRPublicViewKey });
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("api/user-settings/{id}")]
        public IActionResult SetUserSettings(string id, [FromBody]UserSettingsAjaxModel model)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                //if the user logged in is the same as user we want to get/set.
                if (userId == id)
                {
                    using (DBEntities dbe = new DBEntities())
                    {
                        var user = dbe.Users.SingleOrDefault(u => u.Id == id);
                        user.BTCXPUB = model.BTCXPUB;
                        user.LTCXPUB = model.LTCXPUB;
                        user.ETHAccount = model.ETHAccount;
                        user.XMRAddress = model.XMRAddress;
                        user.XMRPrivateViewKey = model.XMRPrivateViewKey;
                        user.XMRPublicViewKey = model.XMRPublicViewKey;
                        dbe.SaveChanges(); //updated in database
                        return Ok();

                    }
                }
                else
                {
                    return Unauthorized();

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
