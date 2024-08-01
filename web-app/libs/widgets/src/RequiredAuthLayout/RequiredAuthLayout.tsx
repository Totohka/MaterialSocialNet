'use client';

import { useRouter } from 'next/navigation';
import { PropsWithChildren, useLayoutEffect } from 'react';
import { getRoute, RoutesEnum } from '@socnet/shared';

export const RequiredAuthLayout = ({ children }: PropsWithChildren) => {
  const router = useRouter();
  useLayoutEffect(() => {
    const isAuth = !!localStorage.getItem('token');

    if (!isAuth) {
      return router.push(getRoute(RoutesEnum.Login));
    }
  }, []);

  return children;
};
