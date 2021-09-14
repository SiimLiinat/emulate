using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class EmulatorMapper
    {
        public static BLL.App.DTO.Emulator MapToBll(EmulatorAdd emulatorAdd)
        {
            var res = new BLL.App.DTO.Emulator
            {
                PlatformId = emulatorAdd.PlatformId,
                Name = emulatorAdd.Name,
                Url = emulatorAdd.Url,
                Picture = emulatorAdd.Picture
            };
            return res;
        }
        
        public static BLL.App.DTO.Emulator MapToBll(APIs.Emulator emulator)
        {
            var res = new BLL.App.DTO.Emulator
            {
                Id = emulator.Id,
                PlatformId = emulator.PlatformId,
                Name = emulator.Name,
                Url = emulator.Url,
                Picture = emulator.Picture,
            };
            return res;
        }
        
        public static APIs.Emulator MapToApi(BLL.App.DTO.Emulator emulator)
        {
            var res = new APIs.Emulator
            {
                Id = emulator.Id,
                PlatformId = emulator.PlatformId,
                Name = emulator.Name,
                Url = emulator.Url,
                PlatformName = emulator.Platform!.Name,
                Picture = emulator.Picture,
                ProgramCount = emulator.Compatibilities!.Count
            };
            return res;
        }
    }
}