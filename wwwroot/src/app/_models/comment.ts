import { User } from "app/_models";

export class Comment {
    id: number;
    text: string;
    authorId: number;
    user: User;
};