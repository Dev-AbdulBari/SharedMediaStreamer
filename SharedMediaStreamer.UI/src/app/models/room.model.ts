import { User } from "./user.model";

export interface Room {
    roomId: string;
    users: User[];
}
