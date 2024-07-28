export enum RoutesEnum {
  Login,
  Registration,
  Chats,
  ChatId,
  PostId,
  ProfileId,
  Users,
  UserId,
  Settings,
  Gallery,
  News,
  Dashboard,
}

type RouteFn = (id?: string) => string;

const Routes: Record<RoutesEnum, RouteFn> = {
  [RoutesEnum.Login]: () => '/login',
  [RoutesEnum.Registration]: () => '/registration',
  [RoutesEnum.Chats]: () => '/chats',
  [RoutesEnum.Users]: () => `/users`,
  [RoutesEnum.Settings]: () => `/settings`,
  [RoutesEnum.Gallery]: () => `/gallery`,
  [RoutesEnum.News]: () => `/news`,
  [RoutesEnum.Dashboard]: () => `/dashboard`,
  [RoutesEnum.UserId]: (id) => `/users/${id}`,
  [RoutesEnum.ChatId]: (id) => `/chats/${id}`,
  [RoutesEnum.PostId]: (id) => `/post/${id}`,
  [RoutesEnum.ProfileId]: (id) => `/profile/${id}`,
};

export const getRoute = (routeKey: RoutesEnum, id?: string) =>
  Routes[routeKey](id ?? '');

// TODO: Что-то адекватное придумать с роутером
// type StaticRoute =
//   | RoutesEnum.Login
//   | RoutesEnum.Registration
//   | RoutesEnum.Chats
//   | RoutesEnum.Users
//   | RoutesEnum.Settings
//   | RoutesEnum.Gallery
//   | RoutesEnum.News
//   | RoutesEnum.Dashboard;

// type DynamicRoute =
//   | RoutesEnum.User
//   | RoutesEnum.ChatId
//   | RoutesEnum.Post
//   | RoutesEnum.Profile;

// type getRouteArgs =
//   | {
//       routeKey: DynamicRoute;
//       id: string;
//     }
//   | { routeKey: StaticRoute; id?: null };

// export const getRoute = ({ routeKey, id }: getRouteArgs) =>
//   Routes[routeKey](id ?? '');
