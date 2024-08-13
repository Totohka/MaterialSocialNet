import { WhoEnum } from '../../models';

/** PUT /api/user Body */
export type UserPutBody = {
  id: number;
  email: string | null;
  firstName: string | null;
  lastName: string | null;
  city: string | null;
  country: string | null;
  dateBirthday: Date | string | null;
  status: string | null;
  phone: string | null;
  avatar: string | null;
  background: string | null;
  // Новый JWT токен
};

/** GET /api/user/{id} Return */
export type UserGetReturn = {
  id: number;
  firstName: string | null;
  lastName: string | null;
  dateBirthday: Date | string;
  city: string | null;
  country: string | null;
  status: string | null;
  email: string | null;
  isSubscriber: boolean;
  avatar: string | null;
  background: string | null;
};

/** GET /api/user Query */
export type UserGetListQuery = {
  search: string;
  number: number;
  who: WhoEnum;
};

/** GET /api/user Return */
export type UserGetListReturn = {
  countAllUsers: number;
  pageCount: number;
  numberPage: number;
  users: UserGetReturn[];
};
