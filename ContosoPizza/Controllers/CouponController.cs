using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class CouponController(PromotionsContext ctx) : ControllerBase
{
    [HttpGet]
    public IEnumerable<Coupon> Get()
    {
        return ctx.Coupons
            .AsNoTracking()
            .ToList();
    }
}