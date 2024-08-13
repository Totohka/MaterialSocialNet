import { ReactElement } from 'react';
import cx from 'classnames';
import styles from './Icon.module.scss';

export interface IconProps {
  iconSvg: ReactElement;
  size: 16 | 24 | 32 | 44;
  className?: string;
}

export function Icon(props: IconProps) {
  const size = props.size || 16;
  return (
    <span
      role="img"
      className={cx(styles.icon, props.className)}
      style={{
        width: `${size}px`,
        height: `${size}px`,
      }}
    >
      {props.iconSvg}
    </span>
  );
}
