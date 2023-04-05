import { MessageEventType } from "../enums/message-event-type";

export interface ChatMessageEvent {
    messageEventType: MessageEventType;
    message: String;
    user: String;
}
