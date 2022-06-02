export interface Post{
    id?: number;
    userId: number;
    content: string;
    doc?: Date;
    visibility: number;
    likes?: string;    
}