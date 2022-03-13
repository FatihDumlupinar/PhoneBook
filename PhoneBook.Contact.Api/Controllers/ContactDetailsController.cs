using Microsoft.AspNetCore.Mvc;
using PhoneBook.Contact.Dtos.Enums;
using PhoneBook.Contact.Dtos.Model.ContactDetail;
using PhoneBook.Contact.Entities.Db;
using PhoneBook.Contact.Repositories.Interfaces;
using PhoneBook.Shared.Common.Helpers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PhoneBook.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactDetailsController : ControllerBase
    {
        #region Ctor&Fields

        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IContactDetailRepository _contactDetailRepository;

        public ContactDetailsController(IContactInfoRepository contactInfoRepository, IContactDetailRepository contactDetailRepository)
        {
            _contactInfoRepository = contactInfoRepository;
            _contactDetailRepository = contactDetailRepository;
        }

        #endregion

        #region Add

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(ContactDetailModel model)
        {
            var dateTimeNow = DateTime.Now;

            //eğer eklenmek istenen detay bilgisi varsa
            var checkContactDetails = await _contactDetailRepository.GetAsync(i => i.IsActive && i.ContactId == model.ContactId && i.ContactTypeId == model.ContactTypeId);
            if (checkContactDetails != default)
            {
                //önce sil
                checkContactDetails.UpdateDate = dateTimeNow;
                checkContactDetails.IsActive = false;

                await _contactDetailRepository.UpdateAsync(checkContactDetails);
                
                //sonra çıkar
            }

            await _contactDetailRepository.AddAsync(new ContactDetail()
            {
                CreateDate = dateTimeNow,
                IsActive = true,
                ContactId = model.ContactId,
                ContactTypeId = model.ContactTypeId,
                Text = model.ContactDetailText

            });

            await _contactInfoRepository.SaveChangeAsync();

            return Ok();
        }

        #endregion

        #region Delete

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var getOldData = await _contactDetailRepository.GetAsync(i => i.IsActive && i.Id == Id);
            if (getOldData == default)
            {
                return NotFound();
            }

            getOldData.UpdateDate = DateTime.Now;
            getOldData.IsActive = false;

            await _contactDetailRepository.UpdateAsync(getOldData);
            await _contactDetailRepository.SaveChangeAsync();

            return Ok();
        }

        #endregion

        #region GetAllDetailsByContactId

        [HttpGet("GetAllDetailsByContactId/{Id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<ContactDetailListModel>))]
        public async Task<IActionResult> GetAllDetailsByContactId(Guid Id)
        {
            var getAllData = await _contactDetailRepository.GetListAsync(i => i.IsActive && i.ContactId == Id);

            var responseModel = getAllData.Select(i => new ContactDetailListModel()
            {
                ContactDetailId = i.Id,
                ContactDetailContactTypeId = i.ContactTypeId,
                ContactDetailContactTypeText = EnumHelper<StaticContactTypeEnm>.GetDisplayValue(EnumHelper<StaticContactTypeEnm>.Parse(i.ContactTypeId.ToString())),
                ContactDetailText = i.Text
            });

            return Ok(responseModel);
        }

        #endregion

    }
}
