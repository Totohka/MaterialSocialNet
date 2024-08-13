export const ROUTES = {
  LOGIN: '/login',
  CREATE_CHAT: '/create-chat',
  REGISTRATION: '/registration',
  CHATS: '/chats',
  USERS: '/users',
  SETTINGS: '/settings',
  NEWS: '/news',
  PROFILE: '/profile',
  GALLERY_ID: (id: string) => `/gallery/${id}`,
  USER_ID: (id: string) => `/users/${id}`,
  CHAT_ID: (id: string) => `/chats/${id}`,
  POST_ID: (id: string) => `/post/${id}`,
};
