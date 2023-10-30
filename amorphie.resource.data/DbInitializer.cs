using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using amorphie.core.Base;

namespace amorphie.resource.data;

public static class DbInitializer
{
    public static void Initialize(ResourceDBContext context)
    {
        context.Database.EnsureCreated();

        // Look for any students.
        if (context!.Resources!.Any())
        {
            return; // DB has been seeded
        }

        var tags = new List<string>
        {
            "tag1",
            "tag2"
        };

        var resourceId1 = Guid.NewGuid();

        var translationResourceDisplay1 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Tüm hesapları listeleme",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };

        var translationResourceDescription1 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Müşteri referansına göre hesapları listeler",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };


        var resourceId2 = Guid.NewGuid();

        var translationResourceDisplay2 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Belirli bir hesap getirme",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };

        var translationResourceDescription2 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Müşteri referansı ve hesap referansına göre hesap bilgilerini getirir",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };


        var resourceId3 = Guid.NewGuid();

        var translationResourceDisplay3 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Tüm Bakiye bilgilerini getirme",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };

        var translationResourceDescription3 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Müşterilerin hesap bakiyelerini getirir",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };


        var resourceId4 = Guid.NewGuid();

        var translationResourceDisplay4 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Belirli bir hesabın bakiye bilgisini getirme",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };

        var translationResourceDescription4 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Müşterinin belirli bir hesabının bakiye bilgisini getirir",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };


        var resourceId5 = Guid.NewGuid();

        var translationResourceDisplay5 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Hesap hareketleri",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };

        var translationResourceDescription5 = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Belirli bir hesabın hareketlerini getirir",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };


        var translationRoleTitleHesap = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Hesap",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };

        var translationRoleTitleBakiye = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Bakiye",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };

        var translationRoleTitleHesapHareket = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Hesap Hareketleri",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };

        var translationRoleGroupTitle = new Translation
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.NewGuid(),
            CreatedByBehalfOf = null,
            Id = Guid.NewGuid(),
            Label = "Açık Bankacılık Hesap Hizmetleri",
            Language = "tr-TR",
            ModifiedAt = DateTime.Now,
            ModifiedBy = Guid.NewGuid(),
            ModifiedByBehalfOf = null,
        };

        var resources = new Resource[]{
            new Resource{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = resourceId1,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                Url = "/fora/DigitalServices/AccountService.svc/hesaplar/([^/]+)",
                Type = core.Enums.HttpMethodType.GET,
                ModifiedByBehalfOf = null,
                Tags = tags.ToArray(),
                Descriptions =  new List<Translation>{translationResourceDescription1}.ToArray(),
                DisplayNames =  new List<Translation>{translationResourceDisplay1}.ToArray(),
                },
                new Resource{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = resourceId2,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                Url = "/fora/DigitalServices/AccountService.svc/hesaplar/([^/]+)/([^/]+)",
                Type = core.Enums.HttpMethodType.GET,
                ModifiedByBehalfOf = null,
                Tags = tags.ToArray(),
                Descriptions =  new List<Translation>{translationResourceDescription2}.ToArray(),
                DisplayNames =  new List<Translation>{translationResourceDisplay2}.ToArray(),
                },
                new Resource{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = resourceId3,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                Url = "/fora/DigitalServices/AccountService.svc/hesaplar/([^/]+)/bakiye",
                Type = core.Enums.HttpMethodType.GET,
                ModifiedByBehalfOf = null,
                Tags = tags.ToArray(),
                Descriptions =  new List<Translation>{translationResourceDescription3}.ToArray(),
                DisplayNames =  new List<Translation>{translationResourceDisplay3}.ToArray(),
                },
                new Resource{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = resourceId4,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                Url = "/fora/DigitalServices/AccountService.svc/hesaplar/([^/]+)/bakiye/([^/]+)",
                Type = core.Enums.HttpMethodType.GET,
                ModifiedByBehalfOf = null,
                Tags = tags.ToArray(),
                Descriptions =  new List<Translation>{translationResourceDescription4}.ToArray(),
                DisplayNames =  new List<Translation>{translationResourceDisplay4}.ToArray(),
                },
                new Resource{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = resourceId5,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                Url = "/fora/DigitalServices/AccountService.svc/hesaplar/([^/]+)/islemler?hesapIslemBslTrh=([^/]+)&hesapIslemBtsTrh=([^/]+)",
                Type = core.Enums.HttpMethodType.GET,
                ModifiedByBehalfOf = null,
                Tags = tags.ToArray(),
                Descriptions =  new List<Translation>{translationResourceDescription5}.ToArray(),
                DisplayNames =  new List<Translation>{translationResourceDisplay5}.ToArray(),
                }
            };

        foreach (Resource r in resources)
        {
            context!.Resources!.Add(r);
        }

        var hesapRoleId = Guid.NewGuid();
        var bakiyeRoleId = Guid.NewGuid();
        var hesapHareketRoleId = Guid.NewGuid();

        var roles = new Role[]{
            new Role{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = hesapRoleId,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                Tags = tags.ToArray(),
                Titles = new List<Translation>{translationRoleTitleHesap}.ToArray(),
                },
                new Role{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = bakiyeRoleId,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                Tags = tags.ToArray(),
                Titles = new List<Translation>{translationRoleTitleBakiye}.ToArray(),
                },
                new Role{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = hesapHareketRoleId,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                Tags = tags.ToArray(),
                Titles = new List<Translation>{translationRoleTitleHesapHareket}.ToArray(),
                }
            };

        foreach (Role r in roles)
        {
            context!.Roles!.Add(r);
        }

        var roleGroupId1 = Guid.NewGuid();

        var roleGroups = new RoleGroup[]{
            new RoleGroup{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = roleGroupId1,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                Tags = tags.ToArray(),
                Titles = new List<Translation>{translationRoleGroupTitle}.ToArray(),
                }
            };

        foreach (RoleGroup r in roleGroups)
        {
            context!.RoleGroups!.Add(r);
        }

        var roleGroupRoles = new RoleGroupRole[]{
            new RoleGroupRole{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                RoleGroupId = roleGroupId1,
                RoleId = hesapRoleId
                },
                new RoleGroupRole{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                RoleGroupId = roleGroupId1,
                RoleId = bakiyeRoleId
                },
                new RoleGroupRole{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                RoleGroupId = roleGroupId1,
                RoleId = hesapHareketRoleId
                }
            };

        foreach (RoleGroupRole r in roleGroupRoles)
        {
            context!.RoleGroupRoles!.Add(r);
        }

        var resourceRoles = new ResourceRole[]{
            new ResourceRole{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                ResourceId =resourceId1,
                RoleId = hesapRoleId
                },
                new ResourceRole{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                ResourceId = resourceId2,
                RoleId = hesapRoleId
                },
                new ResourceRole{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                ResourceId = resourceId3,
                RoleId = bakiyeRoleId
                },
                new ResourceRole{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                ResourceId = resourceId4,
                RoleId = bakiyeRoleId
                },
                new ResourceRole{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                Status = "A",
                ModifiedByBehalfOf = null,
                ResourceId = resourceId5,
                RoleId = hesapHareketRoleId
                }
            };

        foreach (ResourceRole r in resourceRoles)
        {
            context!.ResourceRoles!.Add(r);
        }

        var privilegeId = Guid.NewGuid();

        var privileges = new Privilege[]{
            new Privilege{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = privilegeId,
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                ModifiedByBehalfOf = null,
                Url = "http://localhost:3000/fora/DigitalServices/AccountService.svc/accounts/match/{header.customerId}/{header.ibanNumber}",                
                }
            };

        foreach (Privilege r in privileges)
        {
            context!.Privileges!.Add(r);
        }

            var resourcePrivileges = new ResourcePrivilege[]{
            new ResourcePrivilege{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                ModifiedByBehalfOf = null,
                Status = "A",
                Ttl = 1000,
                PrivilegeId = privilegeId,
                ResourceId = resourceId1                
                },
                  new ResourcePrivilege{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                ModifiedByBehalfOf = null,
                Status = "A",
                Ttl = 1000,
                PrivilegeId = privilegeId,
                ResourceId = resourceId2                
                },
                  new ResourcePrivilege{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                ModifiedByBehalfOf = null,
                Status = "A",
                Ttl = 1000,
                PrivilegeId = privilegeId,
                ResourceId = resourceId3                
                },
                  new ResourcePrivilege{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                ModifiedByBehalfOf = null,
                Status = "A",
                Ttl = 1000,
                PrivilegeId = privilegeId,
                ResourceId = resourceId4                
                },
                  new ResourcePrivilege{
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                CreatedByBehalfOf = null,
                Id = Guid.NewGuid(),
                ModifiedAt = DateTime.Now,
                ModifiedBy = Guid.NewGuid(),
                ModifiedByBehalfOf = null,
                Status = "A",
                Ttl = 1000,
                PrivilegeId = privilegeId,
                ResourceId = resourceId5                
                },
            };

        foreach (ResourcePrivilege r in resourcePrivileges)
        {
            context!.ResourcePrivileges!.Add(r);
        }

        context.SaveChanges();

    }
}