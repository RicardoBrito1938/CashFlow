using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using Tag = CashFlow.Communication.Enums.Tag;

namespace CashFlow.Application.AutoMapper;

public class AutoMapping: Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>().ForMember(dest => dest.Tags, config => config.MapFrom(src => src.Tags.Distinct()));
        CreateMap<RequestRegisterUserJson, User>().ForMember(user => user.Password, config => config.Ignore());
        CreateMap<Communication.Enums.Tag, CashFlow.Domain.Entities.Tag>().ForMember(dest => dest.Value, config => config.MapFrom(src => src));
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseExpenseJson>().ForMember(dest => dest.Tags, config => config.MapFrom(src => src.Tags.Select(tag => tag.Value)));
        CreateMap<Expense, ResponseRegisteredExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<User, ResponseProfileJson>();
    }
}