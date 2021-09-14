export default interface IAppUser {
    id: string;
    email: string;
    userName: string;
    passwordHash: string;
    securityStamp: string;
    lockoutEnd: string | null;
    profilePicture: string | null;
}
