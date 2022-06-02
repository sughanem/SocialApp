export interface User{
    id: number;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    password: string;
    DOB: Date;
    gender: string;
    photos: Uint8Array[];
    about: string;
    token: string;
}