using AutoMapper;

namespace SimpleCure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<BusinessLayer.Models.ResponseBase, SimpleCure.Models.ResponseBase>()
                .ReverseMap();
            CreateMap<BusinessLayer.Models.LotsPurchasedModels.LotsPurchased_Models, SimpleCure.Models.LotPurchasedModels.LotsPurchased_ViewModel>()
             .ReverseMap();
            CreateMap<BusinessLayer.Models.Generic<BusinessLayer.Models.LotsPurchasedModels.LotsPurchased_Models>, SimpleCure.Models.Generic< SimpleCure.Models.LotPurchasedModels.LotsPurchased_ViewModel>>()
               .ReverseMap();
            //CreateMap<BusinessLayer.Models.LotsPurchasedModels.LotsPurchased_Models, SimpleCure.Models.LotPurchasedModels.LotsPurchased_ViewModel>()
            //   .ReverseMap();



            //CreateMap<PublicWorks.DAL.Models.ResponseBase, PublicWorks.BLL.Models.ResponseBase>()
            //    .ReverseMap();
            //CreateMap<PublicWorks.BLL.Models.ResponseBase, PublicWorks.DAL.Models.ResponseBase>();


            //#region SkyDanceRequest

            //CreateMap<SkyDanceRequest, SkyDanceRequestDTO>()
            //    .ReverseMap();
            ////CreateMap<SkyDanceRequestDTO, SkyDanceRequest>();
            //CreateMap<PublicWorks.DAL.Models.OKCGeneric<SkyDanceRequest>, PublicWorks.BLL.Models.OKCGeneric<SkyDanceRequestDTO>>()
            //    .ReverseMap();
            ////CreateMap<PublicWorks.BLL.Models.OKCGeneric<SkyDanceRequestDTO>, PublicWorks.DAL.Models.OKCGeneric<SkyDanceRequest>>();

            //#endregion

            //#region EMailContactList

            //CreateMap<EMailContactList, EMailContactListDTO>()
            //    .ReverseMap();
            //CreateMap<PublicWorks.DAL.Models.OKCGeneric<EMailContactList>, PublicWorks.BLL.Models.OKCGeneric<EMailContactListDTO>>()
            //    .ReverseMap();

            //#endregion

        }
    }
}