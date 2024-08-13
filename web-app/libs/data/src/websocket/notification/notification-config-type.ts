import {
  NotificationChatReturn,
  NotificationPostReturn,
  SendNotificationReturn,
} from './notification-dto';

export type NotificationConfig = {
  onSendNotificationCallback: (data: SendNotificationReturn) => void;
  onNotificationPostCallback: (data: NotificationPostReturn) => void;
  onNotificationChatCallback: (data: NotificationChatReturn) => void;
};
