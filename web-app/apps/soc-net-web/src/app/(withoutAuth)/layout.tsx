import { WithoutLayout } from '@socnet/widgets';
import { PropsWithChildren } from 'react';

const Layout = ({ children }: PropsWithChildren) => {
  return <WithoutLayout>{children}</WithoutLayout>;
};

export default Layout;
