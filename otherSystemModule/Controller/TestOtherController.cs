using domain.Result;
using infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace otherSystemModule.Controller;


[Route("[controller]/[action]")]
[ApiController]
public class TestOtherController
{


    [OtherSystemsInterface("测试业务接口")]
    [HttpGet]
    public ApiResult test(long id)
    {
        return ApiResult.succeed(id);
    }
    
    
}